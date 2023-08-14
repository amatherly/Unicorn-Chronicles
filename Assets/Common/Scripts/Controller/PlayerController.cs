using Singleton;
using TMPro;
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
        private global::Common.Scripts.Maze.Maze MAZE;

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
        
        
        /// <summary>
        /// UI Key count HUD.
        /// </summary>
        [SerializeField] private TMP_Text myKeyCount;

        /// <summary>
        /// Initializes player properties and references during the start of the game.
        /// </summary>
        private void Start()
        {
            MAZE = GameObject.Find("Maze").GetComponent<global::Common.Scripts.Maze.Maze>();
            myCharacterController = GetComponent<CharacterController>();
            myAnimator = GetComponent<Animator>();
            myAudioSource = GetComponent<AudioSource>();
            myCameraTransform = GameObject.Find("CM vcam2").transform;
            
            // Set initial movement and rotation speed values
            mySpeed = 50f;
            myRotationSpeed = 5f;
            
            // Allow the player to move by default
            myCanMove = true;
            
            // Initialize the item count to zero
            myItemCount = 0;
        }

        /// <summary>
        /// Updates the player's movement and interactions based on user input.
        /// </summary>
        private void Update()
        {
            myKeyCount.SetText(myItemCount.ToString());
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
            set => mySpeed = value;
        }

        /// <summary>
        /// Gets or sets a flag indicating whether the player can move.
        /// </summary>
        public bool MyCanMove
        {
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
        
    }
    
}