using System.Collections;
using System.Collections.Generic;
using Singleton;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private Door myDoor;

    private QuestionFactory myQuestionFactory;

    private Maze myMaze;

    private void Start()
    {
        myDoor = GetComponent<Door>();
        myQuestionFactory = QuestionFactory.MyInstance;
        myMaze = GameObject.Find("Maze").GetComponent<Maze>();
    }

    void Update()
    {
        CheckForInput();
    }

    private void CheckForInput()
    {

        if (Input.GetKeyDown(KeyCode.E) && myDoor.MyProximityTrigger)
        {
            if (!myDoor.MyHasAttempted)
            {
                myDoor.MyHasAttempted = true;
                myQuestionFactory.DisplayWindow();
            }

            if (myDoor.MyOpenState)
            {
                myDoor.Close();
            }
            else if (!myDoor.MyLockState)
            {
                myDoor.Open();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDoor.MyProximityTrigger = true;
            myMaze.MyCurrentDoor = myDoor;
            myDoor.MyNavPopup.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDoor.MyProximityTrigger = false;
            myMaze.MyCurrentDoor = null;
            myDoor.MyNavPopup.SetActive(false);
        }
    }
}
