using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{

    [SerializeField]
    private DoorController myDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDoor.SetProximityTrigger(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDoor.SetProximityTrigger(false);
        }
    }
}

