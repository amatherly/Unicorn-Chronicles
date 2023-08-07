using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class ExitButton : MonoBehaviour
    {
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

