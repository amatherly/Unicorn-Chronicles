using Common.Scripts.Controller;
using Singleton;
using UnityEngine;

namespace Common.Scripts.Maze
{

    /// <summary>
    /// Controller class to handle player interactions with each door's <c>GameObject</c>.
    /// </summary>
    public class DoorController : MonoBehaviour
    {

        /// <summary>
        /// The <c>Door</c> script of the <c>GameObject</c> shared by the <c>DoorController</c>.
        /// </summary>
        private Door myDoor;

        /// <summary>
        /// Reference to the game's <c>QuestionFactory</c> object so that the question window
        /// can be displayed upon the player's interaction with the door.
        /// </summary>
        private QuestionFactory myQuestionFactory;

        /// <summary>
        /// Reference to the game's <c>Maze</c> script.
        /// </summary>
        private global::Maze myMaze;

        /// <summary>
        /// Called before the first frame update.
        /// </summary>
        private void Start()
        {
            myDoor = GetComponent<Door>();
            myQuestionFactory = QuestionFactory.MyInstance;
            myMaze = GameObject.Find("Maze").GetComponent<global::Maze>();
        }

        /// <summary>
        /// Called once per frame.
        /// </summary>
        void Update()
        {
            CheckForInput();
        }

        /// <summary>
        /// Checks to see if the player has interacted with the door <c>GameObject</c>
        /// and makes the correct method call to the corresponding <c>Door</c> script based
        /// on its state.
        /// </summary>
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

        /// <summary>
        /// Called when the player enters the door's collider and sets its state
        /// accordingly.
        /// </summary>
        /// <param name="other">The entity entering the collider.</param>
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

        /// <summary>
        /// Called when the player exits the door's collider and sets its state
        /// accordingly.
        /// </summary>
        /// <param name="other">The entity interacting with the collider.</param>
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