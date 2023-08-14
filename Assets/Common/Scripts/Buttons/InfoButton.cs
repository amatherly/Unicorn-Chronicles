using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.Buttons
{
    /// <summary>
    /// Represents a button that shows an information menu.
    /// </summary>
    public class InfoButton : MonoBehaviour
    {
        /// <summary>
        /// The information menu GameObject to be shown.
        /// </summary>
        public GameObject myInfoMenu; 

        /// <summary>
        /// Called when the object is initialized.
        /// Sets up the button's click event listener.
        /// </summary>
        private void Start()
        {
            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(ShowInfo);
        }

        /// <summary>
        /// Shows the information menu.
        /// </summary>
        void ShowInfo()
        {
            myInfoMenu.SetActive(true);
        }
    }
    
    
}