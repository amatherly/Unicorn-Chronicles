using System;
using Singleton;
using SQLite4Unity3d;
using UnityEngine;

public class Question
{
    
    private int myQuestionID;
    private string myQuestion;
    private string myAnswer;

    
    public Question() // Default Constructor
    {
        
    }
    
    public Question(in int theQuestionID, in string theQuestion, in string theAnswer)
    {
        if (theQuestionID == null || theQuestion == null || theAnswer == null)
        {
            throw new ArgumentException("Parameter can't be null. You passed in ID:" + theQuestionID + " Question: " +
                                        theQuestion + " Answer " + theAnswer);
        }
        else
        {
            myQuestionID = theQuestionID;
            myQuestion = theQuestion;
            myAnswer = theAnswer;
        }
    }
    

    public bool CheckUserAnswer(string theAnswerInput)
    {
        Debug.Log("The user answered: " + theAnswerInput + ". The correct answer is: " + myAnswer);
        bool result = theAnswerInput == myAnswer;
        return result;
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