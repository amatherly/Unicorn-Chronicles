/*
 * Unicorn Chronicles: Dark Forest Trivia
 * Summer 2023
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages collectible items in the game.
/// </summary>
/// <author>JJ Coldiron</author>
/// <author>Caroline El Jazmi</author>
/// <author>Brodi Matherly</author>
/// <remarks>
/// Developed using Unity [Version 2021.3.23f1].
/// </remarks>
public class ItemManager : MonoBehaviour
{
    /// <summary>
    /// List of all collectible items in the game.
    /// </summary>
    public List<ItemController> myItemList = new();


    /// <summary>
    /// Automatically populates the list of collectible items and performs initialization.
    /// </summary>
    private void Start()
    {
        // Auto-populate the allMyKeys list with every Key in the scene
        myItemList.AddRange(FindObjectsOfType<ItemController>());
    }

}