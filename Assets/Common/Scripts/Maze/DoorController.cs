using System.Collections;
using System.Collections.Generic;
using Singleton;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private Door myDoor;

    private QuestionFactory myQuestionFactory;

    private Maze myMaze;
    
    public string myDoorID; // Unique door id


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

    
    public void SaveDoorState() 
    { 
        myDoorID = this.gameObject.name;  // Use the door's GameObject name as a unique identifier.

        PlayerPrefs.SetInt(myDoorID + "_LockState", myDoor.MyLockState ? 1 : 0);
        PlayerPrefs.SetInt(myDoorID + "_HasAttempted", myDoor.MyHasAttempted ? 1 : 0);

        // Save position
        PlayerPrefs.SetFloat(myDoorID + "_PosX", transform.position.x);
        PlayerPrefs.SetFloat(myDoorID + "_PosY", transform.position.y);
        PlayerPrefs.SetFloat(myDoorID + "_PosZ", transform.position.z);

        // Save rotation (assuming Euler angles are sufficient)
        PlayerPrefs.SetFloat(myDoorID + "_RotX", transform.eulerAngles.x);
        PlayerPrefs.SetFloat(myDoorID + "_RotY", transform.eulerAngles.y);
        PlayerPrefs.SetFloat(myDoorID + "_RotZ", transform.eulerAngles.z);

        // Save scale (this might not be necessary for doors, but included for completeness)
        PlayerPrefs.SetFloat(myDoorID + "_ScaleX", transform.localScale.x);
        PlayerPrefs.SetFloat(myDoorID + "_ScaleY", transform.localScale.y);
        PlayerPrefs.SetFloat(myDoorID + "_ScaleZ", transform.localScale.z);

        PlayerPrefs.Save();
    } 
      

    
    public void LoadDoorState()
    {
        myDoorID = this.gameObject.name;  // Use the door's GameObject name as a unique identifier.

        // Check if the PlayerPrefs has the necessary key to determine if the door's state was saved previously
        if (PlayerPrefs.HasKey(myDoorID + "_LockState"))
        {
            // Load lock state and attempted state
            myDoor.MyLockState = PlayerPrefs.GetInt(myDoorID + "_LockState") == 1;
            myDoor.MyHasAttempted = PlayerPrefs.GetInt(myDoorID + "_HasAttempted") == 1;

            // Load position
            Vector3 position;
            position.x = PlayerPrefs.GetFloat(myDoorID + "_PosX");
            position.y = PlayerPrefs.GetFloat(myDoorID + "_PosY");
            position.z = PlayerPrefs.GetFloat(myDoorID + "_PosZ");
            transform.position = position;

            // Load rotation
            Vector3 eulerAngles;
            eulerAngles.x = PlayerPrefs.GetFloat(myDoorID + "_RotX");
            eulerAngles.y = PlayerPrefs.GetFloat(myDoorID + "_RotY");
            eulerAngles.z = PlayerPrefs.GetFloat(myDoorID + "_RotZ");
            transform.rotation = Quaternion.Euler(eulerAngles);

            // Load scale
            Vector3 scale;
            scale.x = PlayerPrefs.GetFloat(myDoorID + "_ScaleX");
            scale.y = PlayerPrefs.GetFloat(myDoorID + "_ScaleY");
            scale.z = PlayerPrefs.GetFloat(myDoorID + "_ScaleZ");
            transform.localScale = scale;
        }
        else
        {
            Debug.Log("No saved state found for door with ID: " + myDoorID);
        }
    }
    

}