/*
 * Unicorn Chronicles: Dark Forest Trivia
 * Summer 2023
 */

using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.Buttons
{
    /// <summary>
    /// Represents a button that shows an information menu.
    /// </summary>
    /// <author>JJ Coldiron</author>
    /// <author>Caroline El Jazmi</author>
    /// <author>Brodi Matherly</author>
    /// <remarks>
    /// Developed using Unity [Version 2021.3.23f1].
    /// </remarks>
    public class InfoButton : MonoBehaviour
    {
        /// <summary>
        /// The information menu GameObject to be shown.
        /// </summary>
        [SerializeField] public GameObject myInfoMenu; 

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
        private void ShowInfo()
        {
            myInfoMenu.SetActive(true);
        }
    }
    
    
}