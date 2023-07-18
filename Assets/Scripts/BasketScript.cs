using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketScript : MonoBehaviour
{
    public Transform itemPlacement; // Reference to the position where the item should be placed in the basket

    public void ReceiveItem(GameObject item)
    {
        // Make the item a child of the basket
        item.transform.parent = transform;

        // Adjust the position and rotation of the item within the basket
        item.transform.position = itemPlacement.position;
        item.transform.rotation = itemPlacement.rotation;

        // Disable the item's Rigidbody to prevent it from falling or being affected by physics
        item.GetComponent<Rigidbody>().isKinematic = true;
    }
}


