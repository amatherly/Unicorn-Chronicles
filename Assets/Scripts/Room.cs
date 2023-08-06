using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    private Maze myMaze;

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
        myMaze = GameObject.Find("Maze").GetComponent<Maze>();
        myHasVisited = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Equals(Room theRoom)
    {
        return theRoom.MyRow == myRow && theRoom.MyCol == myCol;
    }

    public int MyRow
    {
        get => myRow;
    }

    public int MyCol
    {
        get => myCol;
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
    }
}
