using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Maze;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Maze : MonoBehaviour
{

    private bool myLoseCondition;

    private Room myCurrentRoom;

    private Door myCurrentDoor;

    [SerializeField]
    private Room[,] myRooms;

    public List<DoorController> myAllDoors; // list of all doors in maze object


    // Start is called before the first frame update
    void Start()
    {
        myRooms = new Room[4, 4];
        PopulateMaze();
        myCurrentRoom = myRooms[3, 0];
    }

    // Update is called once per frame
    void Update()
    {
        if (myLoseCondition)
        {
            Debug.Log("wow you're bad at this");
        }

        if (myCurrentRoom.MyWinRoom)
        {
            Debug.Log("You win!");
        }
    }

    public Room MyCurrentRoom
    {
        get => myCurrentRoom;
        set => myCurrentRoom = value;
    }

    public Room GetDefaultRoom
    {
        get => myRooms[3,0];
    }

    public Door MyCurrentDoor
    {
        get => myCurrentDoor;
        set => myCurrentDoor = value;
    }

    public bool MyLoseCondition
    {
        get => myLoseCondition;
        set => myLoseCondition = value;
    }

    private void PopulateMaze()
    {
        myRooms[0, 0] = GameObject.Find("Room 1-1").GetComponent<Room>();
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

    public bool CheckLoseCondition(int theRow, int theCol, bool[,] theCheck)
    {
        const int NORTH = 0;
        const int EAST = 1;
        const int SOUTH = 2;
        const int WEST = 3;

        bool result = true;
        theCheck[theRow - 1, theCol - 1] = true;
        if (theRow == myCurrentRoom.MyRow && theCol == myCurrentRoom.MyCol)
        {
            result = false;
        }
        else
        {
            for (int i = 0; i < 4 && result; i++)
            {
                if (myRooms[theRow - 1, theCol - 1].MyDoors[i].name != "no-door")
                {
                    Door curr = myRooms[theRow - 1, theCol - 1].MyDoors[i].GetComponent<Door>();

                    if (!curr.MyLockState || !curr.MyHasAttempted)
                    {
                        switch (i)
                        {
                            case NORTH:
                                if (!theCheck[theRow - 2, theCol - 1] && result)
                                {
                                    result = CheckLoseCondition(theRow - 1, theCol, theCheck);
                                }
                                break;
                            case EAST:
                                if (!theCheck[theRow - 1, theCol] && result)
                                {
                                    result = CheckLoseCondition(theRow, theCol + 1, theCheck);
                                }
                                break;
                            case SOUTH:
                                if (!theCheck[theRow, theCol - 1] && result)
                                {
                                    result = CheckLoseCondition(theRow + 1, theCol, theCheck);
                                }
                                break;
                            case WEST:
                                if (!theCheck[theRow - 1, theCol - 2] && result)
                                {
                                    result = CheckLoseCondition(theRow, theCol - 1, theCheck);
                                }
                                break;
                        }
                    }
                }
            }
        }
        return result;
    }

}