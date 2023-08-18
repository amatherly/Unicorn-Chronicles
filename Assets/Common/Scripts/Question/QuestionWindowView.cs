using System;
using Common.Scripts.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Common.Scripts.Question
{
    /// <summary>
    /// Manages the question window's visual elements and interactions.
    /// </summary>
    public class QuestionWindowView : MonoBehaviour
    {
        /// <summary>
        /// The associated controller for the question window.
        /// </summary>
        [SerializeField] private QuestionWindowController myController;

        /// <summary>
        /// The text component displaying the question.
        /// </summary>
        [SerializeField] private TMP_Text myQuestionText;

        /// <summary>
        /// Array of text components for displaying multiple choice button texts.
        /// </summary>
        [SerializeField] private TMP_Text[] myButtonTexts = new TMP_Text[4];

        /// <summary>
        /// The input field for short answer questions.
        /// </summary>
        [SerializeField] private TMP_InputField myInputField;

        /// <summary>
        /// The window displaying the result of the question.
        /// </summary>
        [SerializeField] private GameObject myResultWindow;

        /// <summary>
        /// The text component displaying the result of the question.
        /// </summary>
        [FormerlySerializedAs("resultText")] [SerializeField] private TMP_Text myResultText;

        /// <summary>
        /// The text component displaying the key count.
        /// </summary>
        [SerializeField] private TMP_Text myKeyCountText;
    
        /// <summary>
        /// Initializes the question window view.
        /// </summary>
        public void InitializeView()
        {
            myResultWindow.SetActive(false);
            gameObject.SetActive(true);
            myKeyCountText.SetText(GameObject.FindObjectOfType<PlayerController>().MyItemCount.ToString());
            if (myKeyCountText.text == "0")
            {
                GameObject.Find("HintButton").GetComponent<Button>().enabled = false;
            }
        }

        /// <summary>
        /// Sets the text of the question being displayed.
        /// </summary>
        /// <param name="theQuestionText">The text of the question.</param>
        public void SetQuestionText(in string theQuestionText)
        {
            myQuestionText.SetText(theQuestionText);
        }

        /// <summary>
        /// Sets the text of the multiple choice buttons with provided answers.
        /// </summary>
        /// <param name="theAnswers">An array of answer texts for the buttons.</param>
        public void SetMultipleChoiceButtons(in string[] theAnswers)
        {
            for (int i = 0; i < theAnswers.Length; i++)
            {
                myButtonTexts[i].SetText(theAnswers[i]);
            }
        }

        /// <summary>
        /// Gets the button text for the users answer.
        /// </summary>
        public string GetButtonAnswer(in string theIndex)
        {
            string answerText = myButtonTexts[Int32.Parse(theIndex) - 1].text;
            return answerText;
        }
    
        /// <summary>
        /// Retrieves the text from the input field and sends it to the controller for processing.
        /// </summary>
        public void GetInputFieldText()
        {
            myController.SetAnswerInput(myInputField.text);
            // myInputField.SetTextWithoutNotify(null);
        }
    
        /// <summary>
        /// Displays the result of the user's answer.
        /// </summary>
        /// <param name="theIsCorrect">Whether the user's answer is correct or not.</param>
        public void ShowResult(in bool theIsCorrect)
        {
            myResultWindow.SetActive(true);
            myResultText.SetText(theIsCorrect ? "Correct!" : "Incorrect!");
        }
    }
}