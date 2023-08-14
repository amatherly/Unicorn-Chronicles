using UnityEngine;

namespace Common.Scripts.Buttons
{
    /// <summary>
    /// Represents an exit button in the game user interface.
    /// </summary>
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
}

