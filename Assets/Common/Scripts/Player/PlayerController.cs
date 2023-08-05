using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float mySpeed;
    private bool myCanMove;
    private CharacterController myCharacterController;
    private Animator myAnimator;
    private AudioSource myAudioSource;
    [SerializeField] private GameObject myWinCamera;
    [SerializeField] private GameObject myOverheadCamera;

    private int myItemCount;

    private void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        myAudioSource = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();
        mySpeed = 50f;
        myCanMove = true;
        myItemCount = 0;
    }

    private void Update()
    {
        CheckForInput();

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
                if (!myAudioSource.isPlaying)
                {
                    if (myAudioSource.isPlaying == false)
                    {
                        myAudioSource.Play();
                    }
                }
            }
        }
        else
        {
            myAnimator.SetBool("isWalking", false);
            if (myAudioSource.isPlaying)
            {
                myAudioSource.Stop();
            }
        }
    }

    private void CheckForInput()
    {
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TreasureChest"))
        {
            myAnimator.SetTrigger("Win");
            myCanMove = false;
            Vector3 newRotation = new Vector3(0, -180, 0);
            transform.eulerAngles = newRotation;
            myOverheadCamera.gameObject.SetActive(false);
            myWinCamera.gameObject.SetActive(true);


        }
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