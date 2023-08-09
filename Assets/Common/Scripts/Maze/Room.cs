
using System.Collections.Generic;
using Singleton;
using UnityEngine;

public class Room : MonoBehaviour
{

    [SerializeField]
    private bool myHasVisited;

    [SerializeField]
    private bool myWinRoom;

    [SerializeField]
    private int myRow;

    [SerializeField]
    private int myCol;

    [SerializeField]
    private List<GameObject> myDoors;


    // Start is called before the first frame update
    void Start()
    {
        myHasVisited = false;
    }
    

    public bool Equals(Room theRoom)
    {
        return theRoom.MyRow == myRow && theRoom.MyCol == myCol;
    }

    public int MyRow
    {
        get => myRow;
        set => myRow = value;
    }

    public int MyCol
    {
        get => myCol;
        set => myCol = value;
    }

    public bool MyHasVisited
    {
        get => myHasVisited;
        set => myHasVisited = value;
    }

    public bool MyWinRoom
    {
        get => myWinRoom;
    }

    public List<GameObject> MyDoors
    {
        get => myDoors;
        set => myDoors = value;
    }
}
