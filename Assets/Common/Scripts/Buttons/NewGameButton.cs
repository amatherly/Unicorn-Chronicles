using Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Buttons
{
    public class NewGameButton: MonoBehaviour
    {
        
        public void NextScene()
        {
            SceneManager.LoadScene("Game");
        }
        
        public void OnNewGameButtonClicked()
        {
            // UIControllerInGame.MyInstance.TogglePause();
        }
    }
}