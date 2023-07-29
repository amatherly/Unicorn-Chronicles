using UnityEngine;
using TMPro;
using System;
using System.Linq;
using Singleton;
using Random = System.Random;

public class QuestionWindowView : MonoBehaviour
{
    [SerializeField] private QuestionWindowController myController;
    
    [SerializeField] private TMP_Text myQuestionText;
    [SerializeField] private TMP_Text[] myButtonTexts = new TMP_Text[4];
    [SerializeField] private TMP_InputField myInputField;
    [SerializeField] private GameObject myResultWindow;
    [SerializeField] private TMP_Text resultText;
    
    public void InitializeView()
    {
        myResultWindow.SetActive(false);
        gameObject.SetActive(true);
    }

    public void SetQuestionText(string questionText)
    {
        myQuestionText.SetText(questionText);
    }

    public void SetMultipleChoiceButtons(string[] theAnswers)
    {
        for (int i = 0; i < theAnswers.Length; i++)
        {
            myButtonTexts[i].SetText(theAnswers[i]);
        }
    }

    public string GetButtonAnswer(string theIndex)
    {
        string answerText = myButtonTexts[Int32.Parse(theIndex) - 1].text;
        Debug.Log("Button text: " + answerText);
        return answerText;
    }

    public void EnableInputField()
    {
        myInputField.gameObject.SetActive(true);
    }
    
    public void GetInputFieldText()
    {
        myController.SetAnswerInput(myInputField.text);
        myInputField.SetTextWithoutNotify(null);
    }
    
    public void ShowResult(bool isCorrect)
    {
        myResultWindow.SetActive(true);
        resultText.SetText(isCorrect ? "Correct!" : "Incorrect!");
        Time.timeScale = 1;
    }
}