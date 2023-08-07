using System.Collections;
using System.Collections.Generic;
using Singleton;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private Door myDoor;

    private QuestionFactory myQuestionFactory;

    private Maze myMaze;
    
<<<<<<< Updated upstream
    private Door door;
    
    public string doorID; // Unique door id

=======
    public string doorID;
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
    } 
    
    public void SaveDoorState()
    {
        doorID = this.gameObject.name;  // Use the door's GameObject name as a unique identifier.

        PlayerPrefs.SetInt(doorID + "_LockState", door.MyLockState ? 1 : 0);
        PlayerPrefs.SetInt(doorID + "_HasAttempted", door.MyHasAttempted ? 1 : 0);
=======
    }
    
    public void SaveDoorState() 
    { 
        doorID = this.gameObject.name;  // Use the door's GameObject name as a unique identifier.

        PlayerPrefs.SetInt(doorID + "_LockState", myDoor.MyLockState ? 1 : 0);
        PlayerPrefs.SetInt(doorID + "_HasAttempted", myDoor.MyHasAttempted ? 1 : 0);
>>>>>>> Stashed changes

        // Save position
        PlayerPrefs.SetFloat(doorID + "_PosX", transform.position.x);
        PlayerPrefs.SetFloat(doorID + "_PosY", transform.position.y);
        PlayerPrefs.SetFloat(doorID + "_PosZ", transform.position.z);

        // Save rotation (assuming Euler angles are sufficient)
        PlayerPrefs.SetFloat(doorID + "_RotX", transform.eulerAngles.x);
        PlayerPrefs.SetFloat(doorID + "_RotY", transform.eulerAngles.y);
        PlayerPrefs.SetFloat(doorID + "_RotZ", transform.eulerAngles.z);

        // Save scale (this might not be necessary for doors, but included for completeness)
        PlayerPrefs.SetFloat(doorID + "_ScaleX", transform.localScale.x);
        PlayerPrefs.SetFloat(doorID + "_ScaleY", transform.localScale.y);
        PlayerPrefs.SetFloat(doorID + "_ScaleZ", transform.localScale.z);

        PlayerPrefs.Save();
<<<<<<< Updated upstream
    } 
      
    public void LoadDoorState()
      {
          doorID = this.gameObject.name;  // Use the door's GameObject name as a unique identifier.

          // Check if the PlayerPrefs has the necessary key to determine if the door's state was saved previously
          if (PlayerPrefs.HasKey(doorID + "_LockState"))
          {
              // Load lock state and attempted state
              door.MyLockState = PlayerPrefs.GetInt(doorID + "_LockState") == 1;
              door.MyHasAttempted = PlayerPrefs.GetInt(doorID + "_HasAttempted") == 1;

              // Load position
              Vector3 position;
              position.x = PlayerPrefs.GetFloat(doorID + "_PosX");
              position.y = PlayerPrefs.GetFloat(doorID + "_PosY");
              position.z = PlayerPrefs.GetFloat(doorID + "_PosZ");
              transform.position = position;

              // Load rotation
              Vector3 eulerAngles;
              eulerAngles.x = PlayerPrefs.GetFloat(doorID + "_RotX");
              eulerAngles.y = PlayerPrefs.GetFloat(doorID + "_RotY");
              eulerAngles.z = PlayerPrefs.GetFloat(doorID + "_RotZ");
              transform.rotation = Quaternion.Euler(eulerAngles);

              // Load scale
              Vector3 scale;
              scale.x = PlayerPrefs.GetFloat(doorID + "_ScaleX");
              scale.y = PlayerPrefs.GetFloat(doorID + "_ScaleY");
              scale.z = PlayerPrefs.GetFloat(doorID + "_ScaleZ");
              transform.localScale = scale;
          }
          else
          {
              Debug.Log("No saved state found for door with ID: " + doorID);
          }
      }



=======
    }
    
    public void LoadDoorState()
    {
        doorID = this.gameObject.name;  // Use the door's GameObject name as a unique identifier.

        // Check if the PlayerPrefs has the necessary key to determine if the door's state was saved previously
        if (PlayerPrefs.HasKey(doorID + "_LockState"))
        {
            // Load lock state and attempted state
            myDoor.MyLockState = PlayerPrefs.GetInt(doorID + "_LockState") == 1;
            myDoor.MyHasAttempted = PlayerPrefs.GetInt(doorID + "_HasAttempted") == 1;

            // Load position
            Vector3 position;
            position.x = PlayerPrefs.GetFloat(doorID + "_PosX");
            position.y = PlayerPrefs.GetFloat(doorID + "_PosY");
            position.z = PlayerPrefs.GetFloat(doorID + "_PosZ");
            transform.position = position;

            // Load rotation
            Vector3 eulerAngles;
            eulerAngles.x = PlayerPrefs.GetFloat(doorID + "_RotX");
            eulerAngles.y = PlayerPrefs.GetFloat(doorID + "_RotY");
            eulerAngles.z = PlayerPrefs.GetFloat(doorID + "_RotZ");
            transform.rotation = Quaternion.Euler(eulerAngles);

            // Load scale
            Vector3 scale;
            scale.x = PlayerPrefs.GetFloat(doorID + "_ScaleX");
            scale.y = PlayerPrefs.GetFloat(doorID + "_ScaleY");
            scale.z = PlayerPrefs.GetFloat(doorID + "_ScaleZ");
            transform.localScale = scale;
        }
        else
        {
            Debug.Log("No saved state found for door with ID: " + doorID);
        }
    }
    
>>>>>>> Stashed changes
}
