using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TriviaManager : MonoBehaviour
{
    // Replace this with the URL of your PHP script hosted on a server
    private string phpScriptURL = "http://<ip address>/phpmyadmin";

    [System.Serializable]
    public class QuestionData
    {
        public string Question;
        public string RightAnswer;
    }

    public void FetchQuestionsFromPHP()
    {
        StartCoroutine(GetQuestionsFromPHP());
    }

    private IEnumerator GetQuestionsFromPHP()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(phpScriptURL))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error while fetching data: " + www.error);
            }
            else
            {
                string jsonData = www.downloadHandler.text;
                QuestionData[] questions = JsonUtility.FromJson<QuestionData[]>(jsonData);

                // Now you have the questions and answers data in the 'questions' array
                // You can use this data to display questions and answer options in your game objects
            }
        }
    }

    // Other methods to display questions and handle user input can be implemented here
}
