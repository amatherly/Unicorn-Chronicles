/*
 * Unicorn Chronicles: Dark Forest Trivia
 * Summer 2023
 */

using UnityEngine;

namespace Common.Scripts.Controller
{
    /// <summary>
    /// Simple class to increment the <c>myItemCount</c> field of the
    /// <c>Player</c> script and handle animation.
    /// </summary>
    /// <author>JJ Coldiron</author>
    /// <author>Caroline El Jazmi</author>
    /// <author>Brodi Matherly</author>
    /// <remarks>
    /// Developed using Unity [Version 2021.3.23f1].
    /// </remarks>
    [System.Serializable]
    public class ItemController : MonoBehaviour
    {
        /// <summary>
        /// Pickup sound index.
        /// </summary>
        public static readonly int PICKUP_SOUND_ID = 4;
        
        /// <summary>
        /// Speed of the item's bobbing animation.
        /// </summary>
        public static readonly float SPEED = 15f;

        /// <summary>
        /// Height variance of the item's bobbing animation.
        /// </summary>
        public static readonly float HEIGHT = 0.05f;

        /// <summary>
        /// Reference to the <c>Player</c> script.
        /// </summary>
        private PlayerController myPlayer;

        /// <summary>
        /// Unique ID for each key collectible.
        /// </summary>
        public int myItemID;

        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        void Start()
        {
            myPlayer = FindObjectOfType<PlayerController>();
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        void Update()
        {
            // Moves the item in the pattern of a sin wave.
            Vector3 thePosition = transform.position;
            float newY = Mathf.Sin(Time.time * SPEED) * HEIGHT + thePosition.y;
            thePosition = new Vector3(thePosition.x, newY, thePosition.z);
            transform.position = thePosition;
        }

        /// <summary>
        /// Called when the player enters the item's collider to increment
        /// the player's <c>myItemCount</c> and destroy the <c>GameObject</c>.
        /// </summary>
        /// <param name="theOther"></param>
        private void OnTriggerEnter(Collider theOther) // Unity methods cannot use in parameters!!!
        {
            if (theOther.CompareTag("Player"))
            {
                myPlayer.MyItemCount += 1;
                FindObjectOfType<UIControllerInGame>().PlayUISound(PICKUP_SOUND_ID);
                Destroy(gameObject);
            }
        }
    }
}
