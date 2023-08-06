using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    private Room myRoom;

    private Maze myMaze;

    // Start is called before the first frame update
    void Start()
    {
        myRoom = GetComponent<Room>();
        myMaze = GameObject.Find("Maze").GetComponent<Maze>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myMaze.MyCurrentRoom = myRoom;
            myRoom.MyHasVisited = true;

        }
    }
}
