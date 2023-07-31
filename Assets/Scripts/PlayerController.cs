using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float mySpeed;
    private bool myCanMove;
    private CharacterController myCharacterController;
    private Animator myAnimator;
    private int myItemCount;
    
    private void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
        mySpeed = 50f;
        myCanMove = true;
        myItemCount = 0;
    }
    
    private void Update()
    {
        CheckForInput();
        
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

    private void CheckForInput()
    {

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
