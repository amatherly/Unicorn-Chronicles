using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Buttons
{
    public class InfoButton : MonoBehaviour
    {
        public GameObject InfoPanel; 

        void Start()
        {
            Button btn = this.GetComponent<Button>();
            btn.onClick.AddListener(ShowInfo);
        }

        void ShowInfo()
        {
            InfoPanel.SetActive(true);
        }
    }
    
    
}