using UnityEngine;

namespace Common.Scripts.Controller
{
    /// <summary>
    /// Controls the player character's movement and interactions.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// The singleton instance of the PlayerController class.
        /// </summary>
        private static PlayerController myInstance = null;

        /// <summary>
        /// The movement speed of the player.
        /// </summary>
        private static float mySpeed;

        /// <summary>
        /// The rotation speed of the player.
        /// </summary>
        private static float myRotationSpeed;

        /// <summary>
        /// The Maze instance.
        /// </summary>
        private global::Maze myMaze;

        /// <summary>
        /// The CharacterController component of the player character.
        /// </summary>
        private CharacterController myCharacterController;

        /// <summary>
        /// The Animator component for controlling animations.
        /// </summary>
        private Animator myAnimator;

        /// <summary>
        /// The AudioSource for playing audio.
        /// </summary>
        private AudioSource myAudioSource;

        /// <summary>
        /// A flag indicating whether the player can move.
        /// </summary>
        [SerializeField] private bool myCanMove;

        /// <summary>
        /// The camera transform.
        /// </summary>
        private Transform myCameraTransform;

        /// <summary>
        /// The number of items the player currently holds.
        /// </summary>
        [SerializeField] private int myItemCount;


        private void Start()
        {
            myMaze = GameObject.Find("Maze").GetComponent<global::Maze>();
            myCharacterController = GetComponent<CharacterController>();
            myAnimator = GetComponent<Animator>();
            myAudioSource = GetComponent<AudioSource>();
            myCameraTransform = GameObject.Find("CM vcam2").transform;
            mySpeed = 50f;
            myRotationSpeed = 5f;
            myCanMove = true;
            myItemCount = 0;
        }

        private void Update()
        {
            // Get input values for horizontal and vertical movement
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Create a 3D movement vector using the input values
            Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
            float inputMagnitude = moveDirection.magnitude;

            // Check if the player is moving (inputMagnitude > 0)
            if (inputMagnitude > 0)
            {
                // Set the "isWalking" parameter in the Animator to true, triggering the walking animation
                myAnimator.SetBool("isWalking", true);

                // Check if the player can move
                if (myCanMove)
                {
                    // Play the walking audio if it's not already playing
                    if (!myAudioSource.isPlaying)
                    {
                        myAudioSource.Play();
                    }

                    // Move the character based on input and speed
                    _ = myCharacterController.Move(moveDirection * mySpeed * Time.deltaTime);

                    // Calculate the target angle for rotation based on the input direction
                    float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

                    // Create a target rotation based on the target angle
                    Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

                    // Smoothly interpolate the character's rotation towards the target rotation
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,
                        Time.deltaTime * myRotationSpeed);
                }
            }
            if(myCharacterController.velocity == Vector3.zero || inputMagnitude == 0)
            {
                myAnimator.SetBool("isWalking", false);
                if (myAudioSource.isPlaying)
                {
                    myAudioSource.Stop();
                }
            }
        }


        /// <summary>
        /// Rotates the camera towards a specified object (unused in current version).
        /// </summary>
        /// <param name="theObject">The object to rotate towards.</param>
        public void RotateCameraTowardDoor(Transform theObject)
        {
            // if (Input.GetAxis("Horizontal") > 0)
            // {
            //     myCameraTransform.eulerAngles += new Vector3(myCameraTransform.rotation.x, myCameraTransform.rotation.y + 10 * Time.deltaTime, myCameraTransform.rotation.z);
            //
            // }
            // else
            // {
            //     myCameraTransform.eulerAngles += new Vector3(myCameraTransform.rotation.x, myCameraTransform.rotation.y - 10 * Time.deltaTime, myCameraTransform.rotation.z);
            // }
        }

        /// <summary>
        /// Spends a key item.
        /// </summary>
        /// <returns>True if a key was spent successfully, false otherwise.</returns>
        public bool SpendKey()
        {
            if (myItemCount > 0)
            {
                myItemCount--;
                return true;
            }

            return false;
        }


        /// <summary>
        /// Gets or sets the movement speed of the player.
        /// </summary>
        public float MySpeed
        {
            get => mySpeed;
            set => mySpeed = value;
        }

        /// <summary>
        /// Gets or sets a flag indicating whether the player can move.
        /// </summary>
        public bool MyCanMove
        {
            get => myCanMove;
            set => myCanMove = value;
        }

        /// <summary>
        /// Gets or sets the number of items the player currently holds.
        /// </summary>
        public int MyItemCount
        {
            get => myItemCount;
            set => myItemCount = value;
        }
        
        public static PlayerController MyPlayerController
        {
            get => MyPlayerController;
            set => MyPlayerController = value;
        }
        
    }
}