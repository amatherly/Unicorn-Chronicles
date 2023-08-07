using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Buttons
{
    public class InfoButton : MonoBehaviour
    {
        public GameObject InfoPanel; // Assign in Inspector

        // Start is called before the first frame update
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