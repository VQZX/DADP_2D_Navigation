using UnityEditor;

namespace Waypoint.Editor
{
    [CustomEditor(typeof(WaypointController))]
    public class WaypointControllerEditor : UnityEditor.Editor
    {
        private WaypointController controller;
        
        private void OnSceneGUI()
        {
            // Draw Lines between everything
            var shapeNodesCount = controller.WaypointNodes.Count;
            for (var i = 0; i < shapeNodesCount - 1; i++)
            {
                var nextIndex = (i + 1) % shapeNodesCount;
                var current = controller.WaypointNodes[i];
                var next = controller.WaypointNodes[nextIndex];
                Handles.DrawLine(current.position, next.position);
            }
        }

        private void OnEnable()
        {
            controller = (WaypointController)target;
        }
    }
}