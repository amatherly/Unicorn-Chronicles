using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    private PlayerController myPlayer;

    private bool myHasVisited;

    [SerializeField]
    private int myRow;

    [SerializeField]
    private int myCol;

    private List<Door> myDoors = new List<Door>();


    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myPlayer.MyCurrentRoom = this;
            myHasVisited = true;
        }
    }
}
