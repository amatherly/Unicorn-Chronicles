using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Controller
{
    /// <summary>
    /// Manages collectible items in the game.
    /// </summary>
    public class CollectibleController : MonoBehaviour
    {
        /// <summary>
        /// List of all collectible items in the game.
        /// </summary>
        public List<ItemController>
            allMyItems = new List<ItemController>(); 
        
        
        /// <summary>
        /// Automatically populates the list of collectible items and performs initialization.
        /// </summary>
        private void Start()
        {
            // Auto-populate the allMyKeys list with every Key in the scene
            allMyItems.AddRange(FindObjectsOfType<ItemController>());

            // Testing, print out all the key IDs
            foreach (var currKey in allMyItems)
            {
                Debug.Log("Key ID: " + currKey.myItemID);
            }
        }
        
    }
    
}