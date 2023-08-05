// © 2019 Flying Saci Game Studio
// written by Sylker Teles

using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The Loot class.
/// </summary>
[Serializable]
public class Loot
{
    /// <summary>
    /// The loot GameObject prefab.
    /// </summary>
    public GameObject loot;

    /// <summary>
    /// The loot drop chance. 1 to 100% chance of drop.
    /// </summary>
    [Range(0,1)] public float dropChance;
}

/// <summary>
/// These are the ways you can open the box.
/// </summary>
public enum OpeningMethods { OpenOnCollision, OpenOnKeyPress, OpenOnTouch }

/// <summary>
/// Loot Box class.
/// </summary>
public class LootBox : MonoBehaviour
{

    public OpeningMethods openingMethod;


    public string playerTag = "Player";


    public KeyCode keyCode = KeyCode.Space;


    public bool bouncingBox = true;


    public bool closeOnExit;


    public List<Loot> boxContents = new List<Loot>();


    private bool isPlayerAround;


    public bool isOpen { get; set; }


    Animator animator;


    public event Action <GameObject[]> OnBoxOpen;


    void Start()
    {
        // gets the animator
        animator = GetComponent<Animator>();

        // set the animation to bounce or not
        BounceBox(bouncingBox);
    }

    private void Update()
    {
        // when player is close enough to open the box
        if (isPlayerAround)
        {
            // in case of Key Press method for opening the box,
            // waits for a key to be pressed
            if (Input.GetKey(keyCode)) Open();
        }
    }


    public void BounceBox (bool bounceIt)
    {
        // flag the animator property "bounce" accordingly
        if (animator) animator.SetBool("bounce", bounceIt);
    }

    public void Open ()
    {
        // avoid opening when it's already open
        if (isOpen) return;
        isOpen = true;

        // play the open animation
        if (animator) animator.Play("Open");
        //
        // // calculates the chance of each loot inside the box
        // // and pupulates a list with all received treasures
        //
        // // creates a temp list to store the earned items
        // List<GameObject> loots = new List<GameObject>();
        //
        // // check each prize in this box
        // foreach (Loot loot in boxContents)
        // {
        //     // roll the dice for a chance to win
        //     float chance = UnityEngine.Random.Range(0.0f, 1.0f);
        //
        //     // if win add the item to the temp list
        //     if (loot.dropChance >= chance)
        //     {
        //         // Debug.Log("You got " + loot.loot.name);
        //         loots.Add(loot.loot);
        //     }
        // }
        //
        // // empty the box
        // boxContents.Clear();
        //
        // // calls the OnBoxOpen event and deliver the
        // // earned GameObjects on temp list
        // OnBoxOpen?.Invoke(loots.ToArray());
    }


    public void Close()
    {
        // avoid closing when it's already open
        if (!isOpen) return;
        isOpen = false;

        // play the close animation
        if (animator) animator.Play("Close");
    }

    private void OnMouseDown()
    {
        // checks if the opening method is OpenOnTouch
        if (openingMethod != OpeningMethods.OpenOnTouch) return;

        // Open the box.
        Open();
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (openingMethod == OpeningMethods.OpenOnTouch) return;

        if (collision.gameObject.tag == playerTag)
        {
            Open();
            if (openingMethod == OpeningMethods.OpenOnKeyPress) isPlayerAround = true;
            else Open();
        }
   
    }

    private void OnTriggerExit(Collider collision)
    {
        isPlayerAround = false;
        if (closeOnExit) Close();
    }
}
