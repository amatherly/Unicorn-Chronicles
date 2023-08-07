using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float mySpeed;
    private bool myCanMove;
    private CharacterController myCharacterController;
    private Animator myAnimator;
    private int myItemCount;
    private Maze myMaze; 
<<<<<<< Updated upstream
=======
    public Transform playerCharacterTransform; 

>>>>>>> Stashed changes

    
    private void Start()
    {
        myMaze = GameObject.Find("Maze").GetComponent<Maze>(); // NEW
        myCharacterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
        mySpeed = 50f;
        myCanMove = true;
        myItemCount = 0;
<<<<<<< Updated upstream
        myMaze = GameObject.Find("Maze").GetComponent<Maze>(); // NEW

=======
        
>>>>>>> Stashed changes
    }
    
    private void Update()
    {
        
        // Get input axes
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical);

        float inputMagnitude = moveDirection.magnitude;

        if (inputMagnitude > 0)
        {
            myAnimator.SetBool("isWalking", true);
            if (myCanMove)
            {
                _ = myCharacterController.Move(new Vector3(moveHorizontal, 0, moveVertical) * mySpeed * Time.deltaTime);
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * mySpeed);
            }
        }
        else
        {
            myAnimator.SetBool("isWalking", false);
        }
    }
    
<<<<<<< Updated upstream
=======
    
>>>>>>> Stashed changes
    public void SaveGame()
    {
        if (myMaze == null)
        {
            Debug.LogError("Maze object is not initialized.");
            return;
        }
    
        // Save Player specific data
        PlayerPrefs.SetFloat("PlayerSpeed", mySpeed);
        PlayerPrefs.SetInt("PlayerItemCount", myItemCount);
        PlayerPrefs.SetString("PlayerPosition", JsonUtility.ToJson(transform.position));
<<<<<<< Updated upstream
        // Save door states in maze
=======
        
       
>>>>>>> Stashed changes
        foreach (var door in myMaze.doorsInMaze)
        {
            door.SaveDoorState();
        }
        
        
        // TODO Save the questions state
        PlayerPrefs.Save();
    }
<<<<<<< Updated upstream

=======
    
>>>>>>> Stashed changes
    public void LoadGame()
    {
        // Load Player specific data
        transform.position = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("PlayerPosition"));
<<<<<<< Updated upstream
        mySpeed = PlayerPrefs.GetFloat("PlayerSpeed", mySpeed); 
        myItemCount =
            PlayerPrefs.GetInt("PlayerItemCount", myItemCount); 
        // Load door states in maze
=======
        mySpeed = PlayerPrefs.GetFloat("PlayerSpeed", mySpeed); // Provide a default value (the current speed)
        myItemCount =
            PlayerPrefs.GetInt("PlayerItemCount", myItemCount); // Provide a default value (the current item count)

>>>>>>> Stashed changes
        foreach (var door in myMaze.doorsInMaze)
        {
            door.LoadDoorState();
        }
        
        // TODO Load the questions state 

    }
<<<<<<< Updated upstream
=======
    
    
    /**
     * Deletes saved data (previously saved game) and
     * allows user to start a new game.
     * TODO FINISH ME :)
     */
    public void NewGame()
    {
        // Clear saved data from PlayerPrefs
        // Delete Player specific saved data
        PlayerPrefs.DeleteKey("PlayerSpeed");
        PlayerPrefs.DeleteKey("PlayerItemCount");
        PlayerPrefs.DeleteKey("PlayerPosition");
        
        // Delete saved states for each door
        foreach (var door in myMaze.doorsInMaze)
        {
            string doorID = door.gameObject.name;  // Use the door's GameObject name as a unique identifier.
            PlayerPrefs.DeleteKey(doorID + "_LockState");
            PlayerPrefs.DeleteKey(doorID + "_HasAttempted");
            PlayerPrefs.DeleteKey(doorID + "_PosX");
            PlayerPrefs.DeleteKey(doorID + "_PosY");
            PlayerPrefs.DeleteKey(doorID + "_PosZ");
            PlayerPrefs.DeleteKey(doorID + "_RotX");
            PlayerPrefs.DeleteKey(doorID + "_RotY");
            PlayerPrefs.DeleteKey(doorID + "_RotZ");
            PlayerPrefs.DeleteKey(doorID + "_ScaleX");
            PlayerPrefs.DeleteKey(doorID + "_ScaleY");
            PlayerPrefs.DeleteKey(doorID + "_ScaleZ");
        }
        
        // PlayerController
        // Set Position
        Vector3 defaultPosition = new Vector3(505, 1, 619); // default character position
        transform.position = defaultPosition; 
        // Set Scale
        Vector3 newScale = new Vector3(1, 1, 1);
        playerCharacterTransform.localScale = newScale;
        
        
        // mySpeed = 50f;
        // myCanMove = true;
        // myItemCount = 0;
        //
        // // Maze
        // myMaze.MyLoseCondition = false;
        // myMaze.MyCurrentRoom = myMaze.GetDefaultRoom;
        //
        // // Doors
        // foreach (var door in myMaze.doorsInMaze)
        // {
        //     Door doorComponent = door.GetComponent<Door>();
        //
        //     if (doorComponent != null)
        //     {
        //         doorComponent.MyOpenState = false;
        //         doorComponent.MyLockState = true;
        //         doorComponent.MyHasAttempted = false;
        //         doorComponent.MyProximityTrigger = false;
        //
        //         // Reset the door's rotation
        //         doorComponent.transform.rotation = Quaternion.Euler(doorComponent.MyStartingRotation);
        //     }
        // }

    }
>>>>>>> Stashed changes

    public float MySpeed
    {
        set => mySpeed = value;
    }

    public bool MyCanMove
    {
        set => myCanMove = value;
    }

    public int MyItemCount
    {
        get => myItemCount;
        set => myItemCount = value;
    }
}
