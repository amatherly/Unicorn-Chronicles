// using System.Collections;
// using JetBrains.Annotations;
// using NUnit.Framework;
// using UnityEngine.TestTools;
//
//
// public class QuestionTests 
// {
//     [NotNull]private static readonly int QUESTION_ID = 1;
//     [NotNull] private static readonly string QUESTION_STRING_1 = "This is a question?";
//     [NotNull]private static readonly string ANSWER = "test";
//
//     private readonly Question myTestQuestion1  = new(QUESTION_ID, QUESTION_STRING_1, ANSWER);
//
//     [SetUp]
//     public void BaseSetUp()
//     {
//
//     }
//     
//     [Test]
//     public void CheckAnswerTest()
//     {
//         Assert.False(myTestQuestion1.CheckUserAnswer("wrongAnswer"));
//         Assert.True(myTestQuestion1.CheckUserAnswer("test"));
//     }
//     
//     [Test]
//     public void ConstructorTest()
//     {
//         Assert.NotNull(myTestQuestion1);
//         Assert.AreEqual(QUESTION_ID, myTestQuestion1.MyQuestionID);
//         Assert.AreEqual(QUESTION_STRING_1, myTestQuestion1.MyQuestion);
//         Assert.AreEqual(ANSWER, myTestQuestion1.MyAnswer);
//     }
//
//     [UnityTest]
//     public IEnumerator QuestionTestsWithEnumeratorPasses()
//     {
//
//         yield return null;
//     }
// }
