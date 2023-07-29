using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    private Maze myMaze;

    [SerializeField]
    private bool myHasVisited;

    private bool myWinCondition;

    [SerializeField]
    private int myRow;

    [SerializeField]
    private int myCol;

    [SerializeField]
    private ArrayList myDoors;


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

    public string GetQuestion()
    {
        return "default text, no question loaded";
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

    public ArrayList MyDoors
    {
        get => myDoors;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myMaze.MyCurrentRoom = this;
            myHasVisited = true;
            if (myWinCondition)
            {
                //win window or something idk
            }
        }
    }
}
