using System;
using System.Collections.Generic;
using UnityEngine;

namespace Waypoint
{
    public class WaypointClickNavigation : MonoBehaviour
    {
        [SerializeField]
        protected WaypointController waypointController;
        
        [SerializeField]
        protected Camera mainCamera;
        
        // Speed in meters per second
        [SerializeField]
        protected float speedMPS = 3;

        private float timeToGoal;
        private float currentTime;

        private Transform currentNode;
        private Transform goalNode;
        private List<Transform> path;

        public enum CharacterState
        {
            Idle,
            Moving,
            Talking
        }

        [SerializeField]
        protected CharacterState state;
        
        private void Awake()
        {
            currentNode = waypointController.GetClosestNode(transform.position);
        }

        private void Update()
        {
            if (state != CharacterState.Talking)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // Check closest path
                    var mousePosition = Input.mousePosition;
                    var worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                    worldPosition.z = 0;
                    var endNode = waypointController.GetClosestNode(worldPosition);
                    path = waypointController.GetPath(currentNode, endNode);
                    SetNextPointInPath();
                    state = CharacterState.Moving;
                }
            }

            if (state == CharacterState.Moving)
            {
                currentTime += Time.deltaTime;
                if (currentTime > timeToGoal)
                {
                    transform.position = goalNode.position;
                    currentTime = 0;
                    SetNextPointInPath();
                }
                var percentage = currentTime / timeToGoal;
                var nextPosition = Vector3.Lerp(currentNode.position, goalNode.position, percentage);
                transform.position = nextPosition;
            }
        }

        private void SetNextPointInPath()
        {
            if (path.Count < 2)
            {
                state = CharacterState.Idle;
                return;
            }

            currentNode = path[0];
            goalNode = path[1];
            
            path.RemoveAt(0);

            timeToGoal = Vector2.Distance(currentNode.position, goalNode.position) / speedMPS;
        }
    }
}