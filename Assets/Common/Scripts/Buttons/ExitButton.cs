/*
 * Unicorn Chronicles: Dark Forest Trivia
 * Summer 2023
 */

using UnityEngine;


/// <summary>
/// Represents an exit button in the game user interface.
/// </summary>
/// <author>JJ Coldiron</author>
/// <author>Caroline El Jazmi</author>
/// <author>Brodi Matherly</author>
/// <remarks>
/// Developed using Unity [Version 2021.3.23f1].
/// </remarks>
public class ExitButton : MonoBehaviour
{
    /// <summary>
    /// Exits the game application.
    /// </summary>
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit(); // quit function for built game
#endif
    }
    
    
}



