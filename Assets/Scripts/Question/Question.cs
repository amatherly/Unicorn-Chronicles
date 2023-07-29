using Singleton;
using SQLite4Unity3d;
using UnityEngine;
using UnityEngine.UI;

public class Question
{
    public int myQuestionID;
    private string myQuestion;
    private string myAnswer;
    
    public bool CheckUserAnswer(string theAnswerInput)
    {
        QuestionFactory.MyInstance.RemoveCurrentQuestion();
        Debug.Log("The user answered: " + theAnswerInput + ". The correct answer is: " +  myAnswer); 
        return theAnswerInput == myAnswer;
    }

	
    public int MyQuestionID
    {
        get => myQuestionID;
        set => myQuestionID = value;
    }

    public string MyQuestion
    {
        get => myQuestion;
        set => myQuestion = value;
    }
	
    public string MyAnswer
    {
        get => myAnswer;
        set => myAnswer = value;
    }
	

    public override string ToString()
    {
        return "Question: " + myQuestion + " Answer: " + myAnswer + " ID: " + myQuestionID;
    }
}