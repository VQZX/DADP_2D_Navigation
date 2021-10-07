using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Shapes
{
    public class AreaController : MonoBehaviour
    {
        [SerializeField]
        protected List<Transform> shapeNodes;
        public List<Transform> ShapeNodes => shapeNodes;

        [SerializeField]
        protected List<Vector2> shapeVertices;
        public List<Vector2> ShapeVertices { get; set; }

        [SerializeField]
        protected List<LineSegment> shapeEdges;

        private void Awake()
        {
            // Store points
            shapeVertices = new List<Vector2>();
            foreach (var node in shapeNodes)
            {
                shapeVertices.Add(node.position);
            }
            
            // Generate Edges
            var shapeVerticesCount = shapeVertices.Count;
            for (var i = 0; i < shapeVerticesCount; i++)
            {
                var currentVertex = shapeVertices[i];
                // Edge case: Last point loops back to initial point
                if (i == shapeVerticesCount - 1)
                {
                    shapeEdges.Add(new LineSegment(currentVertex, shapeVertices[0]));
                    continue;
                }
                var nextVertex = shapeVertices[i + 1];
                shapeEdges.Add(new LineSegment(currentVertex, nextVertex));
            }
        }

        public bool IsPointInShape(Vector2 point)
        {
            var lineSegment = new LineSegment(point, point + Vector2.right * float.MaxValue);
            
            // Loop through all edges
            foreach (var edge in  shapeEdges)
            {
                if (LineSegment.IsIntersecting(lineSegment, edge))
                {
                    return true;
                }
            }
            return false;
        }
    }
}