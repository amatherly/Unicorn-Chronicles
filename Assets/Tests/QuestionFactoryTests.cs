using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using Singleton;
using UnityEngine;
using UnityEngine.TestTools;

public class QuestionFactoryTests
{
    [NotNull] private static readonly Question QUESTION_1 = new(1, "Test1?", "1", false);
    [NotNull] private static readonly Question QUESTION_2 = new(2, "Test2?", "2", false);
    [NotNull] private static readonly Question QUESTION_3 = new(3, "Test3?", "3", false);

    private QuestionFactory myMockQF;
    private DataService mockDataService;
    private IEnumerable<Question> myMockQuestions;

    [SetUp]
    public void Setup()
    {
        GameObject gameObject = new GameObject();
        myMockQF = gameObject.AddComponent<QuestionFactory>();
        mockDataService = new("testsDB.db");
        
        myMockQF.MyDataService = mockDataService;
        myMockQF.InitializeQuestionArray();
        
        myMockQuestions = new List<Question>
        {
            QUESTION_1,
            QUESTION_2,
            QUESTION_3
        };
    }

    [Test]
    public void InitializeQuestionArray_Test()
    {
        CollectionAssert.AreEqual(myMockQF.MyQuestions as IEnumerable, myMockQuestions);
    }

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

    [Test]
    public void GetRandomQuestion_Test()
    {
        Assert.NotNull(myMockQF.GetRandomQuestion());
    }
}