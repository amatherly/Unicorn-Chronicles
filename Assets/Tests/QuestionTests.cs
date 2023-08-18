using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
    /// <summary>
    /// Unit tests for the Question class.
    /// </summary>
    public class QuestionTests 
    {
        /// <summary>
        /// Constants for test data.
        /// </summary>
        private static readonly int QUESTION_ID = 1;
        private static readonly string QUESTION_STRING_1 = "This is a question?";
        private static readonly string ANSWER = "test";
        private static readonly bool IS_ANSWERED = false;

        /// <summary>
        /// The test instance of the Question class
        /// </summary>
        private readonly Question myTestQuestion1  = new(QUESTION_ID, QUESTION_STRING_1, ANSWER, IS_ANSWERED);
    
        /// <summary>
        /// Tests the CheckUserAnswer method of the Question class.
        /// </summary>
        [Test]
        public void CheckAnswerTest()
        {
            Assert.False(myTestQuestion1.CheckUserAnswer("wrongAnswer"));
            Assert.True(myTestQuestion1.CheckUserAnswer("test"));
        }
    
        /// <summary>
        /// Tests the constructor of the Question class.
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
            Assert.NotNull(myTestQuestion1);
            Assert.AreEqual(QUESTION_ID, myTestQuestion1.MyQuestionID);
            Assert.AreEqual(QUESTION_STRING_1, myTestQuestion1.MyQuestion);
            Assert.AreEqual(ANSWER, myTestQuestion1.MyAnswer);
        }

        /// <summary>
        /// Tests the Question class with an enumerator for Unity's test framework.
        /// </summary>
        [UnityTest]
        public IEnumerator QuestionTestsWithEnumeratorPasses()
        {
            yield return null;
        }
    }
}
