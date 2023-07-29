using System;
using System.Linq;
using Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = System.Random;


public class QuestionWindowController : MonoBehaviour
{
    private static Random RANDOM = new Random();
    private static UIControllerInGame myUIController;
    private static int POPUP_SOUND = 0;
    private static int CORRECT_SOUND = 2;
    private static int INCORRECT_SOUND = 3;
    
    private QuestionWindowView myView;
    private Question myQuestion;
    
    private string myAnswerInput;
    private int myCorrectIndex;
    private bool myIsCorrect;
    
    [SerializeField] private GameObject TFWindowPrefab;
    [SerializeField] private GameObject multipleChoiceWindowPrefab;
    [SerializeField] private GameObject inputFieldWindowPrefab;


    public void InitializeWindow(Question theQuestion)
    {
        UIControllerInGame.MyInstance.PlayUISound(0);
        
        myQuestion = theQuestion;
        Debug.Log("Initializing window with question: " + myQuestion);

        myIsCorrect = false;
        int ID = theQuestion.MyQuestionID;
        
        switch (ID)
        {    case 1:
                InstantiateTFWindow();
                break;
            case 2:
                InstantiateMultipleChoiceWindow();
                break;
            case 3:
                InstantiateInputFieldWindow();
                break;
        }
    }
    
    private void InstantiateTFWindow()
    {
        GameObject multipleChoiceWindow = Instantiate(TFWindowPrefab, transform);
        myView = multipleChoiceWindow.GetComponent<QuestionWindowView>();
        myView.InitializeView();
        myView.SetQuestionText(myQuestion.MyQuestion);
    }

    private void InstantiateMultipleChoiceWindow()
    {
        GameObject multipleChoiceWindow = Instantiate(multipleChoiceWindowPrefab, transform);
        myView = multipleChoiceWindow.GetComponent<QuestionWindowView>();
        myView.InitializeView();

        string[] words = myQuestion.MyAnswer.Split(',');
        string[] randomizedAnswers = words.OrderBy(x => RANDOM.Next()).ToArray();
        myQuestion.MyAnswer = words[0];
        myView.SetQuestionText(myQuestion.MyQuestion);
        myView.SetMultipleChoiceButtons(randomizedAnswers);
    }
    
    private void InstantiateInputFieldWindow()
    {
        GameObject inputFieldWindow = Instantiate(inputFieldWindowPrefab, transform);
        myView = inputFieldWindow.GetComponent<QuestionWindowView>();
        myView.InitializeView();
        myView.SetQuestionText(myQuestion.MyQuestion);
        myView.EnableInputField();
    }

    public void CheckAnswer()
    {
        Debug.Log("Checking the answer for question: " + myQuestion);
        myIsCorrect = myQuestion.CheckUserAnswer(myAnswerInput);
        myView.ShowResult(myIsCorrect);

        if (myIsCorrect)
        {
            UIControllerInGame.MyInstance.PlayUISound(CORRECT_SOUND);
        }
        else
        {
            UIControllerInGame.MyInstance.PlayUISound(INCORRECT_SOUND);
        }
        Destroy(myView.gameObject);
    }

    public void SetAnswerInput(string theAnswerInput)
    {
        Debug.Log("Setting Answer for question: " + myQuestion);
        
        if (myQuestion.MyQuestionID == 2)
        {
            theAnswerInput = myView.GetButtonAnswer(theAnswerInput);
        }
        if (theAnswerInput != null)
        {
            myAnswerInput = theAnswerInput;
            CheckAnswer();
        }
    }
}