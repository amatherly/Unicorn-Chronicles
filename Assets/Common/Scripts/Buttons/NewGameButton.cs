using Common.Scripts.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Scripts.Buttons
{
    public class NewGameButton: MonoBehaviour
    {
        private SaveLoadManager mySaveLoadManager;
        
        public void NextScene()
        {
            SceneManager.LoadScene("Game 2");
        }
        
    }
}