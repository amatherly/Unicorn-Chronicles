
public class Question
{
    private int myQuestionID;
    private string myQuestion;
    private string myAnswer;
    
    // public Question() // Default Constructor for testing
    // {
    //     
    // }
    //
    // public Question(in int theQuestionID, in string theQuestion, in string theAnswer) // Testing only
    // {
    //     myQuestionID = theQuestionID;
    //     myQuestion = theQuestion;
    //     myAnswer = theAnswer;
    // }
    

    public bool CheckUserAnswer(string theAnswerInput)
    {
        bool result = (theAnswerInput == myAnswer);
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
        return $"Question: {myQuestion} Answer: {myAnswer} ID: {myQuestionID}";
    }
}