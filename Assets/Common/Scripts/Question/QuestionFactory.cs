using System.Collections.Generic;
using System.Linq;
using Random = System.Random;
using UnityEngine;

namespace Singleton
{
    /// <summary>
    /// Factory class responsible for managing questions.
    /// </summary>
    public class QuestionFactory : MonoBehaviour
    {

        /// <summary>
        /// The singleton instance of the QuestionFactory.
        /// </summary>
        private static QuestionFactory INSTANCE;

        /// <summary>
        /// The Maze instance used in the game.
        /// </summary>
        private static readonly Maze MAZE;

        /// <summary>
        /// The random number generator instance.
        /// </summary>
        private static readonly Random RANDOM = new();

        /// <summary>
        /// The DataService instance for managing data.
        /// </summary>
        private DataService myDataService;
        

        /// <summary>
        /// The controller for managing question windows.
        /// </summary>
        private QuestionWindowController myQuestionWindowController;

        /// <summary>
        /// The currently selected question.
        /// </summary>
        private Question myCurrentQuestion;

        /// <summary>
        /// The collection of available questions.
        /// </summary>
        private IEnumerable<Question> myQuestions;

        /// <summary>
        /// The list of randomized questions.
        /// </summary>
        private List<Question> myRandomizedQuestions;

        /// <summary>
        /// Indicates whether the game is a new game.
        /// </summary>
        private bool isNewGame = true;
        
        void Start()
        {
            if (isNewGame)
            {
                myDataService = new DataService("data.sqlite");
                InitializeQuestionArray();
                myQuestionWindowController = GetComponent<QuestionWindowController>();
                isNewGame = false;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private void Awake()
        {
            if (INSTANCE != null && INSTANCE != this)
            {
                Debug.Log("There is already an instance of the factory in the scene");
            }
            else
            {
                MyInstance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        /// <summary>
        /// Initializes the question array with unanswered questions.
        /// </summary>
        public void InitializeQuestionArray()
        {
            myQuestions = myDataService.GetQuestions().Where(q => !q.MyIsAnswered);
            myRandomizedQuestions = myQuestions.OrderBy(a => RANDOM.Next()).ToList();
        }

        /// <summary>
        /// Displays a random question window.
        /// </summary>
        public void DisplayWindow()
        {
            myCurrentQuestion = GetRandomQuestion();
            myQuestionWindowController.InitializeWindow(myCurrentQuestion);
        }

        /// <summary>
        /// Retrieves a random question from the list.
        /// </summary>
        /// <returns>The randomly selected question.</returns>
        public Question GetRandomQuestion()
        {
            if (myQuestions != null)
            {
                myCurrentQuestion = myRandomizedQuestions[0];
                return myCurrentQuestion;
            }
            return null;
        }

        /// <summary>
        /// Removes the current question from the list.
        /// </summary>
        public void RemoveCurrentQuestion()
        {
            if (myRandomizedQuestions != null)
            {
                myRandomizedQuestions.RemoveAll(x => x.MyQuestion == myCurrentQuestion.MyQuestion);
            }
            else
            {
                Debug.Log("The question list is empty!");
            }
        }


        /// <summary>
        /// Initializes questions from a saved state.
        /// </summary>
        public void InitializeQuestionsFromSave()
        {
            var allQuestions = myDataService.GetQuestion();
            myQuestions = allQuestions.Where(q => PlayerPrefs.GetInt("QuestionAnswered_" + q.MyQuestionID, 0) == 0);
            myRandomizedQuestions = myQuestions.OrderBy(a => RANDOM.Next()).ToList();
        }


        /// <summary>
        /// Gets or sets the instance of the QuestionFactory singleton class.
        /// </summary>
        public static QuestionFactory MyInstance
        {
            get => INSTANCE;
            private set => INSTANCE = value;
        }

        public object MyRandomizedQuestions
        {
            get => myRandomizedQuestions;
        }

        public object MyQuestions
        {
            get => myQuestions;
        }
        
        public DataService MyDataService
        {
            get => myDataService;
            set => myDataService = value;
        }
        
        public Question MyCurrentQuestion
        {
            get => myCurrentQuestion;
            set => myCurrentQuestion = value;
        }

    }
}