using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    private SpawnItem heldItem;

    public void PlaceItem(SpawnItem item)
    {
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.up;
    }
}
