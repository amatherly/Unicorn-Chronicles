using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Controller
{
    public class CollectibleController : MonoBehaviour
    {
        public List<ItemController>
            myAllItems = new List<ItemController>(); // List to hold references to all keys in the game

        private void Awake()
        {
            // Auto-populate the allKeys list with every Key in the scene
            myAllItems.AddRange(FindObjectsOfType<ItemController>());

            // Just for testing, print out all the key IDs
            foreach (var key in myAllItems)
            {
                Debug.Log("Key ID: " + key.myItemID);
            }
        }
    }
}