<<<<<<< Updated upstream
using System;
using Common.Scripts.Controller;
using Singleton;
using Unity.VisualScripting;
=======
using Common.Scripts.Controller;
>>>>>>> Stashed changes
using UnityEngine;

namespace Models
{
    public class KeyCollectable : MonoBehaviour
    {
        private AudioSource myAudioSource;

        private void Start()
        {
            myAudioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                FindObjectOfType<PlayerController>().MyItemCount += 1;
                myAudioSource.PlayOneShot(myAudioSource.clip);
            }
        }
    }
}
