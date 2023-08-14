using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Scripts.Buttons
{
    /// <summary>
    /// Represents a button that triggers loading the next
    /// scene for a new game.
    /// </summary>
    public class NewGameButton: MonoBehaviour
    {
     
        /// <summary>
        /// Loads the next scene when the button is clicked.
        /// </summary>
        public void NextScene()
        {
            SceneManager.LoadScene("Game 2");
        }
        
    }
}