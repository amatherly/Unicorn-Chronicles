using System.Linq;
using Common.Scripts.Controller;
using UnityEngine;
using Random = System.Random;


namespace Common.Scripts.Question
{
    /// <summary>
    /// Manages the question windows and user interactions.
    /// </summary>
    public class QuestionWindowController : MonoBehaviour
    {
        /// <summary>
        /// The random number generator instance.
        /// </summary>
        private static readonly Random RANDOM = new();

        /// <summary>
        /// Sound index for a correct answer.
        /// </summary>
        private static readonly int CORRECT_SOUND = 2;

        /// <summary>
        /// Sound index for an incorrect answer.
        /// </summary>
        private static readonly int INCORRECT_SOUND = 3;
        

        /// <summary>
        /// The ID for the Multiple Choice question type.
        /// </summary>
        private static readonly int MULT_CHOICE_ID = 2;
        
        /// <summary>
        /// The Maze instance used in the game.
        /// </summary>
        private static global::Common.Scripts.Maze.Maze MAZE;

        /// <summary>
        /// The UI controller instance for the in-game UI.
        /// </summary>
        private UIControllerInGame myUIController;

        /// <summary>
        /// The view associated with the question window.
        /// </summary>
        private QuestionWindowView myView;

        /// <summary>
        /// The currently displayed question.
        /// </summary>
        private Question myCurrentQuestion;

        /// <summary>
        /// The user's input for the answer.
        /// </summary>
        private string myAnswerInput;

        /// <summary>
        /// The index of the correct answer in multiple choice questions.
        /// </summary>
        private int myCorrectIndex;

        /// <summary>
        /// Indicates if the user's answer is correct.
        /// </summary>
        private bool myIsCorrect;

        /// <summary>
        /// The prefab for the True/False question window.
        /// </summary>
        [SerializeField] private GameObject myTFWindowPrefab;

        /// <summary>
        /// The prefab for the Multiple Choice question window.
        /// </summary>
        [SerializeField] private GameObject myMultipleChoiceWindowPrefab;

        /// <summary>
        /// The prefab for the Input Field question window.
        /// </summary>
        [SerializeField] private GameObject myInputFieldWindowPrefab;

        /// <summary>
        /// This method is called on the frame when a script is enabled the first time.
        /// </summary>
        private void Start()
        {
            // Find and assign the Maze instance used in the game
            MAZE = GameObject.Find("Maze").GetComponent<global::Common.Scripts.Maze.Maze>();
        }


        /// <summary>
        /// Initializes the question window with the provided question.
        /// </summary>
        /// <param name="theQuestion">The question to display.</param>
        public void InitializeWindow(global::Common.Scripts.Question.Question theQuestion)
        {
            UIControllerInGame.MyInstance.PlayUISound(0);
            UIControllerInGame.MyInstance.ShowNav(false);
            Time.timeScale = 0;

            myCurrentQuestion = theQuestion;
            myIsCorrect = false;
            int ID = theQuestion.MyQuestionID;

            Debug.Log(string.Format("Instantiating window type {0} with {1}.", myCurrentQuestion.MyQuestionID, myCurrentQuestion));

            switch (ID)
            {
                case 1:
                    InstantiateTFWindow();
                    break;
                case 2:
                    InstantiateMultipleChoiceWindow();
                    break;
                case 3:
                    InstantiateInputFieldWindow();
                    break;
            }
        }

        /// <summary>
        /// Instantiates a True/False question window.
        /// </summary>
        private void InstantiateTFWindow()
        {
            GameObject multipleChoiceWindow = Instantiate(myTFWindowPrefab, transform);
            myView = multipleChoiceWindow.GetComponent<QuestionWindowView>();
            myView.InitializeView();
            myView.SetQuestionText(myCurrentQuestion.MyQuestion);
        }

        /// <summary>
        /// Instantiates a Multiple Choice question window.
        /// </summary>
        private void InstantiateMultipleChoiceWindow()
        {
            GameObject multipleChoiceWindow = Instantiate(myMultipleChoiceWindowPrefab, transform);
            myView = multipleChoiceWindow.GetComponent<QuestionWindowView>();
            myView.InitializeView();

            string[] words = myCurrentQuestion.MyAnswer.Split(',');
            string[] randomizedAnswers = words.OrderBy(x => RANDOM.Next()).ToArray();
            myCurrentQuestion.MyAnswer = words[0];
            myView.SetQuestionText(myCurrentQuestion.MyQuestion);
            myView.SetMultipleChoiceButtons(randomizedAnswers);
        }

        /// <summary>
        /// Instantiates an Input Field question window.
        /// </summary>
        private void InstantiateInputFieldWindow()
        {
            GameObject inputFieldWindow = Instantiate(myInputFieldWindowPrefab, transform);
            myView = inputFieldWindow.GetComponent<QuestionWindowView>();
            myView.InitializeView();
            myView.SetQuestionText(myCurrentQuestion.MyQuestion);
        }

        /// <summary>
        /// Checks the user's answer and handles the result.
        /// </summary>
        public void CheckAnswer()
        {
            myIsCorrect = myCurrentQuestion.CheckUserAnswer(myAnswerInput);
            myView.ShowResult(myIsCorrect);
            MAZE.MyCurrentDoor.MyLockState = !myIsCorrect;

            if (myIsCorrect)
            {
                UIControllerInGame.MyInstance.PlayUISound(CORRECT_SOUND);
            }
            else
            {
                MAZE.MyLoseCondition = MAZE.CheckLoseCondition(1, 4, new bool[4, 4]);
                UIControllerInGame.MyInstance.PlayUISound(INCORRECT_SOUND);
            }


            if (myIsCorrect)
            {
                MAZE.MyCurrentDoor.Open();
            }

            QuestionFactory.MyInstance.RemoveCurrentQuestion();
            Destroy(myView.gameObject);
        }

        /// <summary>
        /// Sets the user's answer input for the question.
        /// </summary>
        /// <param name="theAnswerInput">The user's answer input.</param>
        public void SetAnswerInput(string theAnswerInput)
        {
            Debug.Log("User Answered: " + theAnswerInput);

            if (myCurrentQuestion.MyQuestionID == MULT_CHOICE_ID && !myIsCorrect)
            {
                theAnswerInput = myView.GetButtonAnswer(theAnswerInput);
            }

            myAnswerInput = theAnswerInput;
            CheckAnswer();
        }

        /// <summary>
        /// Uses a key to answer the question automatically.
        /// </summary>
        public void UseKey()
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            myAnswerInput = myCurrentQuestion.MyAnswer;
            if (player.SpendKey())
            {
                myIsCorrect = true;
                SetAnswerInput(myAnswerInput);
            }
        }
    }
}