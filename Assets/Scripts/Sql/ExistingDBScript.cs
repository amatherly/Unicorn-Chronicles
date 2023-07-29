using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ExistingDBScript : MonoBehaviour {

	public Text DebugText;

	// Use this for initialization
	void Start () {
		var ds = new DataService ("Assets/SQLite4Unity3d/TriviaMaseDb.db");
		//ds.CreateDB ();
		var question = ds.GetTFQuestion();
		ToConsole (question.ToString());

		question = ds.GetMultipleChoiceQuestion();
		ToConsole("Getting a multiple choice question");
		ToConsole (question.ToString());

		ds.CreateQuestion ();
		ToConsole("New question has been created");

	}
	
	private void ToConsole(IEnumerable<Question> people){
		foreach (var person in people) {
			ToConsole(person.ToString());
		}
	}

	private void ToConsole(string msg){
		DebugText.text += System.Environment.NewLine + msg;
		Debug.Log (msg);
	}
}
