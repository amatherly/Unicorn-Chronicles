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
        myMaze = GameObject.FindGameObjectWithTag("Maze").GetComponent<Maze>();
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
    }

    public List<GameObject> MyDoors
    {
        get => myDoors;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myMaze.MyCurrentRoom = this;
            myHasVisited = true;
            if (myWinRoom)
            {
                //win message or something idk
            }
        }
    }
}
