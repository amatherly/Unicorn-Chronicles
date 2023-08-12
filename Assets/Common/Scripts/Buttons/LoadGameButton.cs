using Singleton;
using UnityEngine;

namespace Buttons
{
    public class LoadGameButton : MonoBehaviour
    {
        public void OnLoadGameButtonClicked()
        {
            UIControllerInGame.MyInstance.TogglePause();
        }
    }
}