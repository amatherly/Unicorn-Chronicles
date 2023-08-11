using UnityEngine;

namespace Common.Scripts.Controller
{
    public class PlayerController : MonoBehaviour

    {
        // MAKE STATIC
        private static PlayerController myInstance = null;
        private static float mySpeed;
        private static float myRotationSpeed;
        
        
        public SaveLoadManager SaveLoadManagerInstance { get; set; }
        private global::Maze myMaze;
        private CharacterController myCharacterController;
        private Animator myAnimator;
        private AudioSource myAudioSource;

        
        // STATE
        private bool myCanMove;
        private Transform myCameraTransform;
        [SerializeField] private int myItemCount;
        public Transform myCharacterTransform;
        private static DataService myDataService;


        private void Start()
        {
            myMaze = GameObject.Find("Maze").GetComponent<global::Maze>(); // NEW
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
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
            float inputMagnitude = moveDirection.magnitude;

            if (inputMagnitude > 0)
            {
                myAnimator.SetBool("isWalking", true);

                
                if (myCanMove)
                {
                    if (!myAudioSource.isPlaying)
                    {
                        myAudioSource.Play();
                    }
                    _ = myCharacterController.Move(new Vector3(moveHorizontal, 0, moveVertical) * mySpeed *
                                                   Time.deltaTime);
                    float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                    Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,
                        Time.deltaTime * myRotationSpeed);
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

        public bool SpendKey()
        {
            if (myItemCount > 0)
            {
                myItemCount--;
                return true;
            }

            return false;
        }

        public float MySpeed
        {
            get => mySpeed;
            set => mySpeed = value;
        }

        public Animator MyAnimator
        {
            get => myAnimator;
            set => myAnimator = value;
        }

        public float MyRotationSpeed
        {
            get => myRotationSpeed;
            set => myRotationSpeed = value;
        }

        public Transform MyCharacterTransform
        {
            get => myCharacterTransform;
            set => myCharacterTransform = value;
        }

        public bool MyCanMove
        {
            get => myCanMove;
            set => myCanMove = value;
        }

        public int MyItemCount
        {
            get => myItemCount;
            set => myItemCount = value;
        }

        public static PlayerController MyInstance
        {
            get => myInstance;
            set => myInstance = value;
        }
    }
}