
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

using UnityEngine;

namespace Singleton
{
    public class QuestionFactory : MonoBehaviour
    {
        private static QuestionFactory myInstance = null;
        private static Random RANDOM = new Random();
        private static DataService myDataService;

        private Question myCurrentQuestion;
        private bool isNewGame = true;

        private IEnumerable<Question> myQuestions;
        private List<Question> myRandomizedQuestions;

        private QuestionWindowController myQuestionWindowController;
        

        void Start()
        {
            if (isNewGame)
            {
                myDataService = new DataService("TriviaMaseDb.db");
                InitializeQuestionArray();
                myQuestionWindowController = GetComponent<QuestionWindowController>();
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private void Awake()
        {
            if (myInstance != null && myInstance != this)
            {
                Debug.Log("There is already an instance of the factory in the scene");
            }
            else
            {
                MyInstance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void InitializeQuestionArray()
        {
            myQuestions = myDataService.GetQuestion();
            List<Question> temp = myQuestions.ToList();

            foreach (Question q in myQuestions)
            {
                temp.Add(q);
            }
            myRandomizedQuestions = temp.OrderBy(a => RANDOM.Next()).ToList();
        }


        public void DisplayWindow()
        {
            myCurrentQuestion = GetRandomQuestion();
            myQuestionWindowController.InitializeWindow(myCurrentQuestion);
        }

        public Question GetRandomQuestion()
        {
            if (myQuestions != null)
            {
                myCurrentQuestion = myRandomizedQuestions[0];
                return myCurrentQuestion;
            }
            return null;
        }

        public void RemoveCurrentQuestion()
        {
            myRandomizedQuestions.Remove(myCurrentQuestion);
        }
        
        public static QuestionFactory MyInstance
        {
            get => myInstance;
            set => myInstance = value;
        }
    }
}