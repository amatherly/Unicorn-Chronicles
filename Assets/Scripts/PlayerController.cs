using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float mySpeed;
    private bool myCanMove;
    private Room myCurrentRoom;
    private CharacterController myCharacterController;
    private Animator myAnimator;
    private TMP_Text myItemCountText;
    
    [SerializeField]
    private int myItemCount;

    // Start is called before the first frame update
    private void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
        myItemCountText = GetComponentInChildren<TMP_Text>();
        mySpeed = 50f;
        myCanMove = true;
        myItemCount = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckForInput();
    

        // Get input axes
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3 (moveHorizontal, 0, moveVertical);

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

        // myItemCountText.SetText("Items: " + myItemCount.ToString());
    }

    private void CheckForInput()
    {

    }

    public float MySpeed
    {
        get => mySpeed;
        set => mySpeed = value;
    }

    public bool MyCanMove
    {
        get => myCanMove;
        set => myCanMove = value;
    }

    public Room MyCurrentRoom
    {
        get => myCurrentRoom;
        set => myCurrentRoom = value;
    }

    public int MyItemCount
    {
        get => myItemCount;
        set => myItemCount = value;
    }
}
