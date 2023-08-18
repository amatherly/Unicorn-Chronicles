using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    /// <summary>
    /// Unit tests for the QuestionFactory class.
    /// </summary>
    public class QuestionFactoryTests
    {
        /// <summary>
        /// Represents a set of test questions for unit testing purposes.
        /// </summary>
        [NotNull] private static readonly Question QUESTION_1 = new(1, "Test1?", "1", false);
        [NotNull] private static readonly Question QUESTION_2 = new(2, "Test2?", "2", false);
        [NotNull] private static readonly Question QUESTION_3 = new(3, "Test3?", "3", false);

        /// <summary>
        /// Mock instance of the QuestionFactory class for unit testing.
        /// </summary>
        private QuestionFactory myMockQF;
        
        /// <summary>
        /// Mock instance of the DataService class for unit testing.
        /// </summary>
        private DataService mockDataService;
        
        /// <summary>
        /// Collection of mock questions for unit testing.
        /// </summary>
        private IEnumerable<Question> myMockQuestions;

        /// <summary>
        /// Called to connect a mock database with mock questions.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            GameObject gameObject = new GameObject();
            myMockQF = gameObject.AddComponent<QuestionFactory>();
            // Connect to mock questions database
            mockDataService = new("testsDB.db");
        
            // Set the DataService instance for the mock QuestionFactory
            myMockQF.MyDataService = mockDataService;
            
            // Initialize the question array for testing
            myMockQF.InitializeQuestionArray();
        
            // Define the mock questions
            myMockQuestions = new List<Question>
            {
                QUESTION_1,
                QUESTION_2,
                QUESTION_3
            };
        }

        /// <summary>
        /// Test for initializing the question array.
        /// </summary>
        [Test]
        public void InitializeQuestionArray_Test()
        {
            CollectionAssert.AreEqual(myMockQF.MyQuestions as IEnumerable, myMockQuestions);
        }

        /// <summary>
        /// Test for removing the current question.
        /// </summary>
        [Test]
        public void RemoveCurrentQuestion_Test()
        {
            myMockQuestions = new List<Question>
            {
                QUESTION_2,
                QUESTION_3
            };
        
            myMockQF.MyCurrentQuestion = QUESTION_1;
            myMockQF.RemoveCurrentQuestion();
        
            CollectionAssert.AreEquivalent(myMockQF.MyRandomizedQuestions as IEnumerable, myMockQuestions);
        }

        /// <summary>
        /// Test for getting a random question.
        /// </summary>
        [Test]
        public void GetRandomQuestion_Test()
        {
            Assert.NotNull(myMockQF.GetRandomQuestion());
        }
    }
}