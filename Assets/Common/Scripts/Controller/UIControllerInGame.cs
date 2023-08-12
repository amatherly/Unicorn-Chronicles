using TMPro;
using UnityEngine;


namespace Singleton
{
    public class UIControllerInGame : MonoBehaviour
    {
        private static UIControllerInGame myInstance = null;

        [SerializeField] private AudioClip[] myAudioClips;
        [SerializeField] private Texture2D cursorTexture;
        [SerializeField]private AudioSource myAudioSource;
        
        [SerializeField]
        private GameObject myResultWindow;
        private GameObject myPauseMenu;

        private static Maze myMaze;
        private QuestionWindowController myQuestionWindowControllerController;

        private bool myIsPaused;
        

        void Start()
        {
            myMaze = FindObjectOfType<Maze>();
            myPauseMenu = GameObject.Find("PauseMenu");
            myPauseMenu.SetActive(false);
            // myAudioSource = GetComponent<AudioSource>();
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
            Cursor.visible = true;
        }


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

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                myIsPaused = !myIsPaused;
                myPauseMenu.SetActive(myIsPaused);
                Time.timeScale = myIsPaused ? 0f : 1f;
            }
        }

        public void SetWinOrLoseWindow(bool result)
        {
            myResultWindow.SetActive(true);
            string resultText;
            
            if (!result)
            {
                resultText = "You Won! \nPlay Again?";
            }
            else
            {
                resultText = "You Lost! \nPlay Again?";
            }

            myResultWindow.GetComponentInChildren<TMP_Text>().SetText(resultText);
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
        }

        public void PlayUISound(int audioClipIndex)
        {
            myAudioSource.PlayOneShot(myAudioClips[audioClipIndex]);
        }

        public static UIControllerInGame MyInstance
        {
            get => myInstance;
            set => myInstance = value;
        }
        
        public void TogglePause()
        {
            myIsPaused = !myIsPaused;
            myPauseMenu.SetActive(myIsPaused);
            Time.timeScale = myIsPaused ? 0f : 1f;
        }

        public AudioClip[] MyAudioClips
        {
            get => myAudioClips;
        }
    }
}