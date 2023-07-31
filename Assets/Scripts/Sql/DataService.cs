using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;
using System.Linq;

public class DataService
{
    private SQLiteConnection myConnection;

    public DataService(string DatabaseName)
    {
#if UNITY_EDITOR
        var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb =
 new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb =
 Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb =
 Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb =
 Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb =
 Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb =
 Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        myConnection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);
    }

    // public void CreateDB()
    // {
    //     myConnection.DropTable<Question>();
    //     myConnection.CreateTable<Question>();
    //     myConnection.InsertAll(new[]
    //     {
    //         new Question
    //         {
    //             myQuestionID = 1,
    //             MyQuestion = "2 + 4 = 6",
    //             MyAnswer = "true"
    //         },
    //         new Question
    //         {
    //             myQuestionID = 1,
    //             MyQuestion = "2 + 4 = 8",
    //             MyAnswer = "false"
    //         },
    //         new Question
    //         {
    //             myQuestionID = 3,
    //             MyQuestion = "What is 2 + 4?",
    //             MyAnswer = "6"
    //         },
    //         new Question
    //         {
    //             myQuestionID = 3,
    //             MyQuestion = "What is 2 + 4",
    //             MyAnswer = "6"
    //         }
    //     });
    // }

    public IEnumerable<Question> GetQuestion()
    {
        return myConnection.Table<Question>();
    }

    public Question GetTFQuestion()
    {
        return myConnection.Table<Question>().FirstOrDefault(x => x.myQuestionID == 1);
    }

    public Question GetMultipleChoiceQuestion()
    {
        return myConnection.Table<Question>().FirstOrDefault(x => x.myQuestionID == 2);
    }

    public Question GetShortAnswerQuestion()
    {
        return myConnection.Table<Question>().FirstOrDefault(x => x.myQuestionID == 3);
    }

    // public Question CreateQuestion()
    // {
    //     var p = new Question
    //     {
    //     };
    //     myConnection.Insert(p);
    //     return p;
    // }
}