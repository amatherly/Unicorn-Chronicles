using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ExistingDBScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		var ds = new DataService ("Assets/SQLite4Unity3d/TriviaMaseDb.db");
		//ds.CreateDB ();
	}
}
