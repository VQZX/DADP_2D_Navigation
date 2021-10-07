using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using Vector2 = UnityEngine.Vector2;

namespace Shapes.Editor
{
    [CustomEditor(typeof(AreaController))]
    public class AreaControllerSceneEditor : UnityEditor.Editor
    {
        private AreaController controller;
        
        
        
        private void OnSceneGUI()
        {
            // Draw Lines between everything
            var shapeNodesCount = controller.ShapeNodes.Count;
            for (var i = 0; i < shapeNodesCount; i++)
            {
                var nextIndex = (i + 1) % shapeNodesCount;
                var current = controller.ShapeNodes[i];
                var next = controller.ShapeNodes[nextIndex];
                Handles.DrawLine(current.position, next.position);
            }
        }

        private void OnEnable()
        {
            controller = (AreaController)target;
            if (controller.ShapeVertices == null || controller.ShapeNodes.Count != controller.ShapeVertices.Count)
            {
                foreach (var node in controller.ShapeNodes)
                {
                    controller.ShapeVertices.Add(node.position);
                }
            }
        }
    }
}