using Common.Scripts.Question;
using TMPro;
using UnityEngine;

namespace Common.Scripts.Controller
{
    /// <summary>
    /// Singleton class responsible for managing in-game UI elements and game pause functionality.
    /// </summary>
    public class UIControllerInGame : MonoBehaviour
    {
        /// <summary>
        /// The single instance of the UIController.
        /// </summary>
        private static UIControllerInGame myInstance;

        /// <summary>
        /// Array of audio clips for UI sounds.
        /// </summary>
        [SerializeField] private AudioClip[] myAudioClips;

        /// <summary>
        /// Custom cursor texture.
        /// </summary>
        [SerializeField] private Texture2D myCursorTexture;

        /// <summary>
        /// Audio source for UI sounds.
        /// </summary>
        [SerializeField] private AudioSource myAudioSource;

        /// <summary>
        /// Navigation popup GameObject.
        /// </summary>
        [SerializeField] private GameObject myNavPopup;

        /// <summary>
        /// Result window GameObject.
        /// </summary>
        [SerializeField] private GameObject myResultWindow;

        /// <summary>
        /// Pause menu GameObject.
        /// </summary>
        private GameObject myPauseMenu;


        /// <summary>
        /// Question window controller reference.
        /// </summary>
        private QuestionWindowController myQuestionWindowControllerController;

        /// <summary>
        /// Flag indicating if the game is paused.
        /// </summary>
        private bool myIsPaused;
        

        /// <summary>
        /// Initializes the UIControllerInGame instance and sets up initial UI elements.
        /// </summary>
        void Start()
        {
            myPauseMenu = GameObject.Find("PauseMenu");
            myPauseMenu.SetActive(false);
            Cursor.SetCursor(myCursorTexture, Vector2.zero, CursorMode.Auto);
            Cursor.visible = true;
        }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private void Awake()
        {
            if (myInstance != null && myInstance != this)
            {
                Debug.Log("There is already an instance of the UIController in the scene!");
            }
            else
            {
                MyInstance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        /// <summary>
        /// Update is called once per frame and handles game pause functionality.
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                myIsPaused = !myIsPaused;
                myPauseMenu.SetActive(myIsPaused);
                Time.timeScale = myIsPaused ? 0f : 1f;
            }
        }
        
        /// <summary>
        /// Displays the win or lose window based on the result.
        /// </summary>
        /// <param name="theResult">True for a win, false for a loss.</param>
        public void SetWinOrLoseWindow(bool theResult)
        {
            myResultWindow.SetActive(true);
            string resultText;

            if (!theResult)
            {
                resultText = "You Won! \nPlay Again?";
            }
            else
            {
                resultText = "You Lost! \nPlay Again?";
            }

            myResultWindow.GetComponentInChildren<TMP_Text>().SetText(resultText);
        }


        /// <summary>
        /// Shows or hides the navigation popup.
        /// </summary>
        /// <param name="theIsShowing">True to show the popup, false to hide it.</param>
        public void ShowNav(bool theIsShowing)
        {
            myNavPopup.SetActive(theIsShowing);
        }

        /// <summary>
        /// Pauses the game by setting the time scale to 0.
        /// </summary>
        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        /// <summary>
        /// Resumes the game by setting the time scale back to 1.
        /// </summary>
        public void ResumeGame()
        {
            Time.timeScale = 1;
        }

        /// <summary>
        /// Plays a UI sound using the specified audio clip index.
        /// </summary>
        public void PlayUISound(int theAudioClipIndex)
        {
            myAudioSource.PlayOneShot(myAudioClips[theAudioClipIndex]);
        }

        /// <summary>
        /// Gets or sets the instance of the UIControllerInGame singleton class.
        /// </summary>
        public static UIControllerInGame MyInstance
        {
            get => myInstance;
            private set => myInstance = value;
        }
    }
}