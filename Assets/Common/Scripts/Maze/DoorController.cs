using System;
using Common.Scripts.Controller;
using Singleton;
using UnityEngine;

namespace Common.Scripts.Maze
{
    public class DoorController : MonoBehaviour
    {

        private Door myDoor;

        private QuestionFactory myQuestionFactory;

        private global::Maze myMaze;

        private static int myDoorCounter = 0; // Static counter to ensure uniqueness
        public string myDoorID; // Unique Door ID


        private void Start()
        {
            myDoor = GetComponent<Door>();
            myQuestionFactory = QuestionFactory.MyInstance;
            myMaze = GameObject.Find("Maze").GetComponent<global::Maze>();
        }

        private void Awake()
        {
            AssignUniqueID();
        }

        void Update()
        {
            CheckForInput();
        }
        
        private void AssignUniqueID()
        {
            myDoorID = "Door_" + myDoorCounter + "_" + transform.position;
            myDoorCounter++;
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
                PlayerController.MyInstance.RotateCameraTowardDoor(myDoor.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                myDoor.MyProximityTrigger = false;
                myMaze.MyCurrentDoor = null;
                myDoor.MyNavPopup.SetActive(false);
                PlayerController.MyInstance.RotateCameraTowardDoor(PlayerController.MyInstance.transform);
            }
        }

    }
}