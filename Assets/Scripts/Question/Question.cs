using Singleton;
using SQLite4Unity3d;
using UnityEngine;
using UnityEngine.UI;

public class Question
{
    public int myQuestionID;
    private string myQuestion;
    private string myAnswer;
    private Maze myMaze = GameObject.Find("Maze").GetComponent<Maze>();
    
    public bool CheckUserAnswer(string theAnswerInput)
    {
        QuestionFactory.MyInstance.RemoveCurrentQuestion();
        Debug.Log("The user answered: " + theAnswerInput + ". The correct answer is: " +  myAnswer);
        bool result = theAnswerInput == myAnswer;
        myMaze.MyCurrentDoor.MyLockState = !result;
        myMaze.MyLoseCondition = myMaze.CheckLoseCondition(1, 4, new bool[4, 4]);
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