using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Waypoint
{
    public class WaypointController : MonoBehaviour
    {
        [FormerlySerializedAs("shapeNodes")]
        [SerializeField]
        protected List<Transform> waypointNodes;
        public List<Transform> WaypointNodes => waypointNodes;

        public Transform GetClosestNode(Vector2 worldPoint)
        {
            float closestDistance = float.MaxValue;
            Transform currentClosest = null;
            foreach (var node in waypointNodes)
            {
                var distance = Vector2.Distance(worldPoint, node.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    currentClosest = node;
                }
            }
            return currentClosest;
        }

        
        // Allow going backward and forwards
        public List<Transform> GetPath(Transform beginNode, Transform endNode)
        {
            if (!waypointNodes.Contains(beginNode))
            {
                beginNode = waypointNodes[0];
            }
            var list = new List<Transform>();
            var addToPath = false;

            var beginIndex = waypointNodes.IndexOf(beginNode);
            var endIndex = waypointNodes.IndexOf(endNode);

            if (beginIndex < endIndex)
            {
                for (var index = beginIndex; index <= endIndex; index++)
                {
                    var current = waypointNodes[index];
                    list.Add(current);
                }
            }
            else if (endIndex < beginIndex)
            {
                for (var index = endIndex; index >= beginIndex; index--)
                {
                    var current = waypointNodes[index];
                    list.Add(current);
                }
            }
            
            

            return list;
        }
    }
}