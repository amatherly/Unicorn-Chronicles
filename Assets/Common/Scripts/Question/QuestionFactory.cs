/*
 * Unicorn Chronicles: Dark Forest Trivia
 * Summer 2023
 */

using System.Collections.Generic;
using System.Linq;
using Common.Scripts.Editor.Sql;
using UnityEngine;
using Random = System.Random;

namespace Common.Scripts.Question
{
    /// <summary>
    /// Factory class responsible for managing questions.
    /// </summary>
    /// <author>JJ Coldiron</author>
    /// <author>Caroline El Jazmi</author>
    /// <author>Brodi Matherly</author>
    /// <remarks>
    /// Developed using Unity [Version 2021.3.23f1].
    /// </remarks>
    public class QuestionFactory : MonoBehaviour
    {

        /// <summary>
        /// The singleton instance of the QuestionFactory.
        /// </summary>
        private static QuestionFactory INSTANCE;

        /// <summary>
        /// The Maze instance used in the game.
        /// </summary>
        private static readonly global::Common.Scripts.Maze.Maze MAZE;

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
        
        /// <summary>
        /// This method is called on the frame when a script is enabled the first time.
        /// </summary>
        void Start()
        {
            // Check if the game is a new game
            if (isNewGame)
            {
                // Initialize the DataService with the database file
                myDataService = new DataService("data.sqlite");
                
                // Initialize the array of unanswered questions
                InitializeQuestionArray();
                
                // Get the QuestionWindowController component attached to this GameObject
                myQuestionWindowController = GetComponent<QuestionWindowController>();
                
                // Mark that this is not a new game anymore
                isNewGame = false;
            }
        }

        /// <summary>
        /// This method is called immediately after a scene is loaded, and before any other objects are set up.
        /// </summary>
        private void Awake()
        {
            // Check if there is an existing instance of the factory in the scene
            if (INSTANCE != null && INSTANCE != this)
            {
                Debug.Log("There is already an instance of the factory in the scene");
            }
            else
            {
                INSTANCE = this;
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
            IEnumerable<Question> allQuestions = myDataService.GetQuestion();
            myQuestions = allQuestions.Where(q => PlayerPrefs.GetInt("QuestionAnswered_" + q.MyQuestionID, 0) == 0);
            myRandomizedQuestions = myQuestions.OrderBy(a => RANDOM.Next()).ToList();
        }


        /// <summary>
        /// Gets or sets the instance of the QuestionFactory singleton class.
        /// </summary>
        public static QuestionFactory MyInstance
        {
            get => INSTANCE;
        }

        /// <summary>
        /// Gets the list of randomized questions.
        /// </summary>
        public object MyRandomizedQuestions
        {
            get => myRandomizedQuestions;
        }

        /// <summary>
        /// Gets the collection of available questions.
        /// </summary>
        public object MyQuestions
        {
            get => myQuestions;
        }
        
        /// <summary>
        /// Sets the DataService instance for managing data.
        /// </summary>
        public DataService MyDataService
        {
            set => myDataService = value;
        }
        
        /// <summary>
        /// Sets the currently selected question.
        /// </summary>
        public Question MyCurrentQuestion
        {
            set => myCurrentQuestion = value;
        }
    }
}