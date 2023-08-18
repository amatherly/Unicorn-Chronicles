/*
 * Unicorn Chronicles: Dark Forest Trivia
 * Summer 2023
 */

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to handle color change of the doors on the
/// minimap.
/// </summary>
/// <author>JJ Coldiron</author>
/// <author>Caroline El Jazmi</author>
/// <author>Brodi Matherly</author>
/// <remarks>
/// Developed using Unity [Version 2021.3.23f1].
/// </remarks>
public class MinimapDoor : MonoBehaviour
{

    /// <summary>
    /// The <c>Door</c> the minimap cell corresponds to.
    /// </summary>
    [SerializeField] GameObject myDoor;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        ColorChange();
    }

    /// <summary>
    /// Changes the color of the cell depending on the state
    /// of its corresponding <c>Door</c>. Yellow if the door has
    /// not been attempted, green if the question was answered correctly,
    /// and red if the question was answered incorrectly.
    /// </summary>
    private void ColorChange()
    {
        Color newColor = Color.yellow;
        Door door = myDoor.GetComponent<Door>();

        if (door.MyHasAttempted && !door.MyLockState)
        {
            newColor = Color.green;
        }

        if (door.MyHasAttempted && door.MyLockState)
        {
            newColor = Color.red;
        }

        GetComponent<Image>().color = newColor;
    }

}