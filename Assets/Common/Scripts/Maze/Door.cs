using System.Collections;
using UnityEngine;

namespace Common.Scripts.Maze
{
    /// <summary>
    /// Class <c>Door</c> contains state and handles open/close animations.
    /// </summary>
    public class Door : MonoBehaviour
    {

        /// <summary>
        /// Constant for rotate animation speed.
        /// </summary>
        private static readonly float ROTATE_SPEED = 1f;

        /// <summary>
        /// Constant for rotation angle.
        /// </summary>
        private static readonly float ROTATION_AMOUNT = 90f;

        /// <summary>
        /// Boolean indicating whether or not the <c>Door</c> is open.
        /// </summary>
        [SerializeField]
        private bool myOpenState;

        /// <summary>
        /// Boolean indicating whether or not the <c>Door</c> is locked.
        /// </summary>
        [SerializeField]
        private bool myLockState;

        /// <summary>
        /// Boolean indicating whether or not a question has been attempted.
        /// </summary>
        [SerializeField]
        private bool myHasAttempted;

        /// <summary>
        /// Boolean indicating whether the <c>Door</c> is horizontal or vertical so that
        /// the animation will run as expected.
        /// </summary>
        [SerializeField]
        private bool myHorizontalState;

        /// <summary>
        /// Boolean indicating whether or not the player is within proximity of the
        /// <c>Door</c>.
        /// </summary>
        private bool myProximityTrigger;

        /// <summary>
        /// Vector indicating the starting rotation of the door's asset.
        /// </summary>
        private Vector3 myStartingRotation;

        /// <summary>
        /// Reference to the <c>GameObject</c> for the player so that its transform
        /// can be accessed.
        /// </summary>
        private GameObject myPlayer;

        /// <summary>
        /// Instance field for the animation currently underway.
        /// </summary>
        private Coroutine myAnimation;

        /// <summary>
        /// Static counter to ensure a unique String for <c>myDoorID</c>.
        /// </summary>
        private static int myDoorCounter;

        /// <summary>
        /// Unique name so that <c>Door</c> state can be individually captured for save/load.
        /// </summary>
        public string myDoorID; // Unique Door ID


        /// <summary>
        /// Called when the Script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            AssignUniqueID();
        }

        /// <summary>
        /// Called before the first frame update.
        /// </summary>
        void Start()
        {
            myOpenState = false;
            myLockState = true;
            myHasAttempted = false;
            myProximityTrigger = false;
            myStartingRotation = transform.rotation.eulerAngles;
            myPlayer = GameObject.FindGameObjectWithTag("Player");
        }

        /// <summary>
        /// Method contains logic to determine whether or not the <c>DoRotationOpen()</c>
        /// animation should be initiated.
        /// </summary>
        public void Open()
        {
            if (!myOpenState)
            {
                if (myAnimation != null)
                {
                    StopCoroutine(myAnimation);
                }

                myAnimation = StartCoroutine(DoRotationOpen());
            }
        }

        /// <summary>
        /// Method contains logic to determine whether or not the <c>DoRotationClose()</c>
        /// animation should be initiated.
        /// </summary>
        public void Close()
        {
            if (myOpenState)
            {
                if (myAnimation != null)
                {
                    StopCoroutine(myAnimation);
                }

                myAnimation = StartCoroutine(DoRotationClose());
            }
        }

        /// <summary>
        /// Accessor and mutator for the <c>myProximityTrigger</c> field.
        /// </summary>
        public bool MyProximityTrigger
        {
            get => myProximityTrigger;
            set => myProximityTrigger = value;
        }

        /// <summary>
        /// Accessor for the <c>myStartingRotation</c> field.
        /// </summary>
        public Vector3 MyStartingRotation
        {
            get => myStartingRotation; 
        }

        /// <summary>
        /// Accessor and mutator for the <c>myOpenState</c> field.
        /// </summary>
        public bool MyOpenState
        {
            get => myOpenState;
            set => myOpenState = value;
        }

        /// <summary>
        /// Accessor and mutator for the <c>myLockState</c> field.
        /// </summary>
        public bool MyLockState
        {
            get => myLockState;
            set => myLockState = value;
        }

        /// <summary>
        /// Accessor and mutator for the <c>myAnimation</c> field.
        /// </summary>
        public Coroutine MyAnimation
        {
            get => myAnimation;
            set => myAnimation = value;
        }
    
        /// <summary>
        /// Accessor and mutator for the <c>myHasAttempted</c> field.
        /// </summary>
        public bool MyHasAttempted
        {
            get => myHasAttempted;
            set => myHasAttempted = value;
        }

        /// <summary>
        /// Accessor and mutator for the <c>myPlayer</c> field.
        /// </summary>
        public GameObject MyPlayer
        {
            set => myPlayer = value;
        }

        /// <summary>
        /// Assigns unique ID to doors inside maze. 
        /// </summary>
        private void AssignUniqueID()
        {
            myDoorID = "Door_" + myDoorCounter + "_" + transform.position;
            myDoorCounter++;
        }

        /// <summary>
        /// Coroutine for the open animation of the <c>Door</c>.
        /// </summary>
        /// <returns>
        /// Yield return null so that the animation can be resumed on the
        /// next frame update.
        /// </returns>
        private IEnumerator DoRotationOpen()
        {
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation;

            if (myHorizontalState && (myPlayer.transform.position.z > transform.position.z)
                || !myHorizontalState && (myPlayer.transform.position.x > transform.position.x))
            {
                endRotation = Quaternion.Euler(new Vector3(0, myStartingRotation.y - ROTATION_AMOUNT, 0));
            }
            else
            {
                endRotation = Quaternion.Euler(new Vector3(0, myStartingRotation.y + ROTATION_AMOUNT, 0));
            }

            myOpenState = true;
            float time = 0;

            while (time < 1)
            {
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
                yield return null;
                time += Time.deltaTime * ROTATE_SPEED;
            }
        }

        /// <summary>
        /// Coroutine for the close animation of the <c>Door</c>.
        /// </summary>
        /// <returns>
        /// Yield return null so that the animation can be resumed on the
        /// next frame update.
        /// </returns>
        private IEnumerator DoRotationClose()
        {
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.Euler(myStartingRotation);

            myOpenState = false;
            float time = 0;

            while (time < 1)
            {
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
                yield return null;
                time += Time.deltaTime * ROTATE_SPEED;
            }
        }
    
    
    }
}