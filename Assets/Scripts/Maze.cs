using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Maze : MonoBehaviour
{

    private bool myLoseCondition;

    private Room myCurrentRoom;

    [SerializeField]
    private Room[,] myRooms;

    // Start is called before the first frame update
    void Start()
    {
        // Temp until we get rooms going
        MyCurrentRoom = GetComponentInChildren<Room>();

        myRooms = new Room[4, 4];
        PopulateMaze();

    }

    // Update is called once per frame
    void Update()
    {
        CheckLoseCondition();
    }

    public Room MyCurrentRoom
    {
        get => myCurrentRoom;
        set => myCurrentRoom = value;
    }

    // stupid code but whatever
    private void PopulateMaze()
    {
        // myRooms[0, 0] = GameObject.FindGameObjectWithTag("Room 1-1").GetComponent<Room>();
        myRooms[0, 1] = GameObject.FindGameObjectWithTag("Room 1-2").GetComponent<Room>();
        myRooms[0, 2] = GameObject.FindGameObjectWithTag("Room 1-3").GetComponent<Room>();
        myRooms[0, 3] = GameObject.FindGameObjectWithTag("Room 1-4").GetComponent<Room>();
        myRooms[1, 0] = GameObject.FindGameObjectWithTag("Room 2-1").GetComponent<Room>();
        myRooms[1, 1] = GameObject.FindGameObjectWithTag("Room 2-2").GetComponent<Room>();
        myRooms[1, 2] = GameObject.FindGameObjectWithTag("Room 2-3").GetComponent<Room>();
        myRooms[1, 3] = GameObject.FindGameObjectWithTag("Room 2-4").GetComponent<Room>();
        myRooms[2, 0] = GameObject.FindGameObjectWithTag("Room 3-1").GetComponent<Room>();
        myRooms[2, 1] = GameObject.FindGameObjectWithTag("Room 3-2").GetComponent<Room>();
        myRooms[2, 2] = GameObject.FindGameObjectWithTag("Room 3-3").GetComponent<Room>();
        myRooms[2, 3] = GameObject.FindGameObjectWithTag("Room 3-4").GetComponent<Room>();
        myRooms[3, 0] = GameObject.FindGameObjectWithTag("Room 4-1").GetComponent<Room>();
        myRooms[3, 1] = GameObject.FindGameObjectWithTag("Room 4-2").GetComponent<Room>();
        myRooms[3, 2] = GameObject.FindGameObjectWithTag("Room 4-3").GetComponent<Room>();
        myRooms[3, 3] = GameObject.FindGameObjectWithTag("Room 4-4").GetComponent<Room>();
    }

    //FIXME
    private bool CheckLoseCondition()
    {
        return false;
    }

}
