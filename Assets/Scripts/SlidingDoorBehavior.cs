using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorBehavior : MonoBehaviour
{
    // OnTrigger is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.BroadcastMessage("OpenDoor");
            Debug.Log("Player entered trigger zone");
        }
    }

    // OnTrigger is called when the Collider other has stopped touching the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.BroadcastMessage("CloseDoor");
            Debug.Log("Player exited trigger zone");
        }
    }
}