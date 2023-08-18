/*
 * Unicorn Chronicles: Dark Forest Trivia
 * Summer 2023
 */

using System;


/// <summary>
/// Represents a question, ID, and answer pair from a Database.
/// </summary>
/// <author>JJ Coldiron</author>
/// <author>Caroline El Jazmi</author>
/// <author>Brodi Matherly</author>
/// <remarks>
/// Developed using Unity [Version 2021.3.23f1].
/// </remarks>
public class Question
{
    /// <summary>
    /// The unique identifier of the question.
    /// </summary>
    private int myQuestionID;

    /// <summary>
    /// The text of the question.
    /// </summary>
    private string myQuestion;

    /// <summary>
    /// The correct answer to the question.
    /// </summary>
    private string myAnswer;

    /// <summary>
    /// Indicates whether the question has been answered.
    /// </summary>
    private bool myIsAnswered;


    // /// <summary>
    // /// Default constructor for the Question class.
    // /// </summary>
    public Question()
    {
    }

    /// <summary>
    /// Constructor for creating a new Question instance.
    /// </summary>
    /// <param name="theQuestionID">The ID of the question.</param>
    /// <param name="theQuestion">The question text.</param>
    /// <param name="theAnswer">The answer text.</param>
    public Question(in int theQuestionID, in string theQuestion, in string theAnswer, in bool theIsAnswered)
    {
        if (theQuestion == null || theAnswer == null)
        {
            throw new ArgumentException("Parameter can't be null. You passed in ID:" + theQuestionID + " Question: " +
                                        theQuestion + " Answer " + theAnswer);
        }

        myQuestionID = theQuestionID;
        myQuestion = theQuestion;
        myAnswer = theAnswer;
        myIsAnswered = theIsAnswered;
    }


    /// <summary>
    /// Checks if a user-provided answer matches the correct answer.
    /// </summary>
    /// <param name="theAnswerInput">The user's answer input.</param>
    /// <returns>True if the answer is correct, false otherwise.</returns>
    public bool CheckUserAnswer(in string theAnswerInput)
    {
        return string.Equals(theAnswerInput, myAnswer, StringComparison.OrdinalIgnoreCase);
    }


    /// <summary>
    /// Gets or sets the question's ID.
    /// </summary>
    public int MyQuestionID
    {
        get => myQuestionID;
        set => myQuestionID = value;
    }

    /// <summary>
    /// Gets or sets a flag indicating whether the question is answered.
    /// </summary>
    public bool MyIsAnswered
    {
        get => myIsAnswered;
        set => myIsAnswered = value;
    }

    /// <summary>
    /// Gets or sets the question text.
    /// </summary>
    public string MyQuestion
    {
        get => myQuestion;
        set => myQuestion = value;
    }

    /// <summary>
    /// Gets or sets the correct answer text.
    /// </summary>
    public string MyAnswer
    {
        get => myAnswer;
        set => myAnswer = value;
    }

    /// <summary>
    /// Returns a formatted string representation of the question.
    /// </summary>
    /// <returns>The formatted string representation.</returns>
    public override string ToString()
    {
        return $"Question: {myQuestion} Answer: {myAnswer} ID: {myQuestionID}";
    }

    /// <summary>
    /// Overrides the Equals method to compare Question objects by their values.
    /// </summary>
    /// <param name="theOther">The object to compare.</param>
    /// <returns>True if the objects are equal in values, false otherwise.</returns>
    public override bool Equals(object theOther)
    {

        if (theOther == null || GetType() != theOther.GetType())
        {
            return false;
        }

        Question theOtherQuestion = (Question)theOther;
        return theOtherQuestion.MyQuestionID == MyQuestionID && theOtherQuestion.myIsAnswered == MyIsAnswered &&
               theOtherQuestion.MyAnswer.Equals(MyAnswer) && theOtherQuestion.MyQuestion.Equals(MyQuestion);
    }

    /// <summary>
    /// Override of the default hash function.
    /// </summary>
    /// <returns> A hash code for the current object. </returns>
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}