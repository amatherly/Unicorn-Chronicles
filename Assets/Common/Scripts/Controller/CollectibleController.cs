using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Controller
{
    public class CollectibleController : MonoBehaviour
    {
        public List<ItemController>
            allMyItems = new List<ItemController>(); 

        private void Awake()
        {
            // Auto-populate the allKeys list with every Key in the scene
            allMyItems.AddRange(FindObjectsOfType<ItemController>());

            // Just for testing, print out all the key IDs
            foreach (var currKey in allMyItems)
            {
                Debug.Log("Key ID: " + currKey.myItemID);
            }
        }
    }
}