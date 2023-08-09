using System;
using System.Linq;
using Singleton;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;


public class QuestionWindowController : MonoBehaviour
{
    private static Random RANDOM = new Random();
    private  Maze myMaze;
    
    private static int CORRECT_SOUND = 2;
    private static int INCORRECT_SOUND = 3;
    
    private UIControllerInGame myUIController;
    private QuestionWindowView myView;
    private Question myQuestion;
    
    private string myAnswerInput;
    private int myCorrectIndex;
    private bool myIsCorrect;
    
    [FormerlySerializedAs("TFWindowPrefab")] [SerializeField] private GameObject myTFWindowPrefab;
    [FormerlySerializedAs("multipleChoiceWindowPrefab")] [SerializeField] private GameObject myMultipleChoiceWindowPrefab;
    [FormerlySerializedAs("inputFieldWindowPrefab")] [SerializeField] private GameObject myInputFieldWindowPrefab;

    private void Start()
    {
        myMaze = GameObject.Find("Maze").GetComponent<Maze>();
    }

    public void InitializeWindow(Question theQuestion)
    {
        UIControllerInGame.MyInstance.PlayUISound(0);
        
        myQuestion = theQuestion;
        myIsCorrect = false;
        int ID = theQuestion.MyQuestionID;
        
        Debug.Log(string.Format("Instansiating window type {0} with {1}.", myQuestion.MyQuestionID, myQuestion));
        
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
        GameObject multipleChoiceWindow = Instantiate(myTFWindowPrefab, transform);
        myView = multipleChoiceWindow.GetComponent<QuestionWindowView>();
        myView.InitializeView();
        myView.SetQuestionText(myQuestion.MyQuestion);
    }

    private void InstantiateMultipleChoiceWindow()
    {
        GameObject multipleChoiceWindow = Instantiate(myMultipleChoiceWindowPrefab, transform);
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
        GameObject inputFieldWindow = Instantiate(myInputFieldWindowPrefab, transform);
        myView = inputFieldWindow.GetComponent<QuestionWindowView>();
        myView.InitializeView();
        myView.SetQuestionText(myQuestion.MyQuestion);
        myView.EnableInputField();
    }

    public void CheckAnswer()
    {
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
        
        
        myMaze.MyCurrentDoor.MyLockState = !myIsCorrect;
        QuestionFactory.MyInstance.RemoveCurrentQuestion();
        Destroy(myView.gameObject);
    }

    public void SetAnswerInput(string theAnswerInput)
    {
        Debug.Log("User Answered: " + theAnswerInput);

        if (myQuestion.MyQuestionID == 2 && !myIsCorrect)
        {
            theAnswerInput = myView.GetButtonAnswer(theAnswerInput);
        }
        if (theAnswerInput != null)
        {
            myAnswerInput = theAnswerInput;
            CheckAnswer();
        }
    }

    public void UseKey()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player.SpendKey())
        {
            myIsCorrect = true;
            SetAnswerInput(myQuestion.MyAnswer);
        }
        else
        {
            myUIController.PlayUISound(5);
        }
    }
}