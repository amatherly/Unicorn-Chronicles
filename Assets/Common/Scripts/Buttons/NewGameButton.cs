using Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Buttons
{
    public class NewGameButton: MonoBehaviour
    {
        
        public void NextScene()
        {
            SceneManager.LoadScene("Game 2");
        }
        
    }
}