/*
 * Unicorn Chronicles: Dark Forest Trivia
 * Summer 2023
 */

using TMPro;
using UnityEngine;

namespace Common.Scripts.Controller
{
    /// <summary>
    /// Controls the player character's movement and interactions.
    /// </summary>
    /// <author>JJ Coldiron</author>
    /// <author>Caroline El Jazmi</author>
    /// <author>Brodi Matherly</author>
    /// <remarks>
    /// Developed using Unity [Version 2021.3.23f1].
    /// </remarks>
    public class PlayerController : MonoBehaviour
    {

        /// <summary>
        /// The rotation speed of the player.
        /// </summary>
        private static float ROTATE_SPEED;
        
        /// <summary>
        /// The movement speed of the player.
        /// </summary>
        private float mySpeed;

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
            myCharacterController = GetComponent<CharacterController>();
            myAnimator = GetComponent<Animator>();
            myAudioSource = GetComponent<AudioSource>();
            
            ROTATE_SPEED = 5f;

            mySpeed = 40f;
            myCanMove = true;
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
                myAnimator.SetBool("isWalking", true);

                if (myCanMove)
                {
                    // Play the walking audio if it's not already playing
                    if (!myAudioSource.isPlaying)
                    {
                        myAudioSource.Play();
                    }

                    // Move and rotate the character based on input and speed
                    _ = myCharacterController.Move(moveDirection * mySpeed * Time.deltaTime);
                    
                    float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                    Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
                    
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,
                        Time.deltaTime * ROTATE_SPEED);
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
            get => mySpeed;
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