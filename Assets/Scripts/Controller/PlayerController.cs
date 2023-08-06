using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float mySpeed;
    private bool myCanMove;
    private CharacterController myCharacterController;
    private Animator myAnimator;
    private int myItemCount;
    private Maze myMaze; 

    
    private void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
        mySpeed = 50f;
        myCanMove = true;
        myItemCount = 0;
        myMaze = GameObject.Find("Maze").GetComponent<Maze>(); // NEW

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
        // Save door states in maze
        foreach (var door in myMaze.doorsInMaze)
        {
            door.SaveDoorState();
        }
        
        
        // TODO Save the questions state
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        // Load Player specific data
        transform.position = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("PlayerPosition"));
        mySpeed = PlayerPrefs.GetFloat("PlayerSpeed", mySpeed); 
        myItemCount =
            PlayerPrefs.GetInt("PlayerItemCount", myItemCount); 
        // Load door states in maze
        foreach (var door in myMaze.doorsInMaze)
        {
            door.LoadDoorState();
        }
        
        // TODO Load the questions state 

    }

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
