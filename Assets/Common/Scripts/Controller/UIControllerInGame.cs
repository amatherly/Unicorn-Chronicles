using UnityEngine;


namespace Singleton
{
    public class UIControllerInGame : MonoBehaviour
    {
        public AudioClip[] MyAudioClips
        {
            get => myAudioClips;
            set => myAudioClips = value;
        }

        private static UIControllerInGame myInstance = null;

        [SerializeField] private AudioClip[] myAudioClips;

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

            // Find the PauseMenu in scene and store the reference
            PauseMenu = GameObject.Find("PauseMenu");
            myIsPaused = false;
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
        
        // Update is called once per frame
        void Update()
        {
            CheckForKeyboardInput();
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
        }


        void CheckForKeyboardInput()
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

        public void PlayUISound(int audioClipIndex)
        {
            myAudioSource.PlayOneShot(myAudioClips[audioClipIndex]);
        }

        public static UIControllerInGame MyInstance
        {
            get => myInstance;
            set => myInstance = value;
        }
    }
}