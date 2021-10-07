using UnityEngine;

public class DynamicSpriteLayering : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    private const float MAX_Y = 9.62f;
    private const float MIN_Y = -1.15f;

    private const float MIN_ORDER = 0;
    private const float MAX_ORDER = 100;
    
    // Inelegant, but effective hack
    private void LateUpdate()
    {
        int newOrder = (int)Remap(transform.position.y);

        spriteRenderer.sortingOrder = newOrder;
    }

    private float Remap(float value)
    {
        return (value - MAX_Y) / (MIN_ORDER - MAX_Y) * (MAX_ORDER - MIN_Y) + MIN_Y;
    }
}
