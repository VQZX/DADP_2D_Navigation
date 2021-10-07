using System;
using System.Numerics;
using Shapes;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class NavigationClicker : MonoBehaviour
{
    [SerializeField]
    protected Camera mainCamera;

    [SerializeField]
    protected AreaController areaController;

    public enum CharacterState
    {
        Idle,
        Moving,
        Talking
    }

    [SerializeField]
    protected CharacterState state;

    // Speed in meters per second
    [SerializeField]
    protected float speedMPS = 3;

    private float timeToGoal;
    private float currentTime;
    
    private Vector2 previousPosition;
    private Vector2 currentGoal;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Input.mousePosition;
            var worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;

            if (!areaController.IsPointInShape(worldPosition))
            {
                return;
            }

            switch (state)
            {
                case CharacterState.Idle:
                case CharacterState.Moving:
                    previousPosition = transform.position;
                    currentGoal = worldPosition;
                    var magnitude = Vector2.Distance(previousPosition, currentGoal);
                    /*
                     * speed = distance / time
                     * time = distance / speed
                     */
                    
                    timeToGoal = magnitude / speedMPS;
                    state = CharacterState.Moving;
                    currentTime = 0;
                    break;
                case CharacterState.Talking:
                    break;
            }
        }

        if (state == CharacterState.Moving)
        {
            currentTime += Time.deltaTime;
            if (currentTime > timeToGoal)
            {
                transform.position = currentGoal;
                state = CharacterState.Idle;
            }
            float percentage = currentTime / timeToGoal;
            var nextPosition = Vector3.Lerp(previousPosition, currentGoal, percentage);
            transform.position = nextPosition;
        }
    }
}
