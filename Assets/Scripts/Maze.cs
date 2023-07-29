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
        myRooms = new Room[4, 4];
        PopulateMaze();
    }

    // Update is called once per frame
    void Update()
    {
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
        myRooms[0, 1] = GameObject.Find("Room 1-2").GetComponent<Room>();
        myRooms[0, 2] = GameObject.Find("Room 1-3").GetComponent<Room>();
        myRooms[0, 3] = GameObject.Find("Room 1-4").GetComponent<Room>();
        myRooms[1, 0] = GameObject.Find("Room 2-1").GetComponent<Room>();
        myRooms[1, 1] = GameObject.Find("Room 2-2").GetComponent<Room>();
        myRooms[1, 2] = GameObject.Find("Room 2-3").GetComponent<Room>();
        myRooms[1, 3] = GameObject.Find("Room 2-4").GetComponent<Room>();
        myRooms[2, 0] = GameObject.Find("Room 3-1").GetComponent<Room>();
        myRooms[2, 1] = GameObject.Find("Room 3-2").GetComponent<Room>();
        myRooms[2, 2] = GameObject.Find("Room 3-3").GetComponent<Room>();
        myRooms[2, 3] = GameObject.Find("Room 3-4").GetComponent<Room>();
        myRooms[3, 0] = GameObject.Find("Room 4-1").GetComponent<Room>();
        myRooms[3, 1] = GameObject.Find("Room 4-2").GetComponent<Room>();
        myRooms[3, 2] = GameObject.Find("Room 4-3").GetComponent<Room>();
        myRooms[3, 3] = GameObject.Find("Room 4-4").GetComponent<Room>();
    }

    private bool CheckLoseCondition(int theRow, int theCol)
    {
        bool result = true;
        bool[,] hasChecked = new bool[4, 4];
        hasChecked[theRow, theCol] = true;

        for (int i = 0; i < 4; i++)
        {
            if (myRooms[theRow, theCol].MyDoors[i] != null)
            {
                DoorController curr = (DoorController)myRooms[theRow, theCol].MyDoors[i];
                if (!curr.MyLockState || !curr.MyHasAttempted)
                {
                    result = false;
                    switch (i)
                    {
                        case 0:
                            if (!hasChecked[theRow + 1, theCol])
                            {
                                result = CheckLoseCondition(theRow + 1, theCol);
                            }
                            break;
                        case 1:
                            if (!hasChecked[theRow, theCol + 1])
                            {
                                result = CheckLoseCondition(theRow, theCol + 1);
                            }
                            break;
                        case 2:
                            if (!hasChecked[theRow - 1, theCol])
                            {
                                result = CheckLoseCondition(theRow - 1, theCol);
                            }
                            break;
                        case 3:
                            if (!hasChecked[theRow, theCol - 1])
                            {
                                result = CheckLoseCondition(theRow + 1, theCol - 1);
                            }
                            break;
                    }
                }
            }
        }
        return result;
    }

}
