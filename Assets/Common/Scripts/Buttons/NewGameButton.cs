/*
 * Unicorn Chronicles: Dark Forest Trivia
 * Summer 2023
 */

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents a button that triggers loading the next
/// scene for a new game.
/// </summary>
/// <author>JJ Coldiron</author>
/// <author>Caroline El Jazmi</author>
/// <author>Brodi Matherly</author>
/// <remarks>
/// Developed using Unity [Version 2021.3.23f1].
/// </remarks>
public class NewGameButton : MonoBehaviour
{

    /// <summary>
    /// Loads the next scene when the button is clicked.
    /// </summary>
    public void NextScene()
    {
        SceneManager.LoadScene("Game 2");
    }

}