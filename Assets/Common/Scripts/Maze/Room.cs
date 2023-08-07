
using System.Collections.Generic;
using Singleton;
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
    

    public string GetQuestion()
    {
        return "default text, no question loaded";
    }

    public int MyRow
    {
        get => myRow;
    }

    public int MyCol
    {
        get => myCol;
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
