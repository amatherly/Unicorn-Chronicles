using Common.Scripts.Controller;
using UnityEngine;

namespace Models
{
    /// <summary>
    /// Represents a collectible key item in the game.
    /// </summary>
    public class KeyCollectable : MonoBehaviour
    {
        /// <summary>
        /// The AudioSource component used to play audio for player colliding with
        /// key collectible.
        /// </summary>
        private AudioSource myAudioSource;
        
        /// <summary>
        /// Initializes the audio source component.
        /// </summary>
        private void Start()
        {
            myAudioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Handles the logic when the key collectible is triggered by a collider.
        /// </summary>
        /// <param name="theOther">The collider that triggered the interaction.</param>
        private void OnTriggerEnter(Collider theOther)
        {
            if (theOther.gameObject.CompareTag("Player"))
            {
                // Increment the player's key count
                FindObjectOfType<PlayerController>().MyItemCount += 1;
                
                // Play the key collection sound
                myAudioSource.PlayOneShot(myAudioSource.clip);
            }
        }
        

    }
}
