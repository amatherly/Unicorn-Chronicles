using UnityEngine;


namespace Singleton
{
    public class UIControllerInGame : MonoBehaviour
    {

        private static UIControllerInGame myInstance = null;


        [SerializeField] private AudioClip[] myAudioClips;
        [SerializeField] private Texture2D cursorTexture;


        private AudioSource myAudioSource;

        // private PauseMenu myPauseMenu; // old
        private GameObject PauseMenu; // Changed PauseMenu to GameObject
        private QuestionWindowController _myQuestionWindowControllerController;

        private bool myIsPaused;

        void Start()
        {
            // old
            // myAudioSource = GetComponent<AudioSource>();
            // myPauseMenu = GetComponentInChildren<PauseMenu>();
            // myIsPaused = false;

            // new
            myAudioSource = GetComponent<AudioSource>();
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
            Cursor.visible = true;

            // Find the PauseMenu in scene and store the reference
            PauseMenu = GameObject
                .Find("PauseMenu"); // Change "PauseMenu" to the actual name of the GameObject representing your pause menu
            myIsPaused = false;

            // Checking if PauseMenu is found in the scene
            if (PauseMenu == null)
            {
                Debug.LogError("Options GameObject not found in the scene. Please check the GameObject name.");
            }
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
                
                if (PauseMenu != null)
                {
                    // Toggle the visibility of the PauseMenu GameObject based on the isPaused flag
                    PauseMenu.SetActive(myIsPaused);
                    
                    // Pause or unpause the game based on the isPaused flag
                    Time.timeScale = myIsPaused ? 0f : 1f;
                }
            }
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
        
        public AudioClip[] MyAudioClips
        {
            get => myAudioClips;
        }
    }
}