using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Buttons
{
    public class InfoButton : MonoBehaviour
    {
        public GameObject myInfoMenu; 

        private void Start()
        {
            Button btn = this.GetComponent<Button>();
            btn.onClick.AddListener(ShowInfo);
        }

        void ShowInfo()
        {
            myInfoMenu.SetActive(true);
        }
    }
    
    
}