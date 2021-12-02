using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Web;
using UnityEngine;
using System.IO;

//THIS IS THE SAVE MANAGER. THIS FILE IS MADE TO HANDLE LOADING AND SAVING THE FILE
//game manager must load the save file on awake() so it doesnt overwrite with an empty list
public class SaveManager : MonoBehaviour
{
    //make file saveDirectory specifically save into assets folder -- i specifically chose joe folder for testing
    public string saveDirectory = "/Joe/SaveData/";
    //file name with saved datatri
    public string saveGameData =  "MyData.txt";
    // File with username
    public string userNameFile = "Username.txt";
    // current username
    public string userName = "Default";
    //create an empty list to add onto
    public SavedDataObject savedDataObject = null;
    public List<PlayerScore> playerScoreList = null;

    public void Awake() { Load(); }

    // Saves a playerscore to disk.
    public void addScore(PlayerScore ss)
    {
        Debug.Log("SaveManager - Enter addScores");

        //get saveDirectory of file
        string savedDataDirectoryStr = Application.dataPath + saveDirectory;
        //check if exists
        if (!Directory.Exists(savedDataDirectoryStr))
        {
            Debug.Log("Save file does not exist, creating");
            Directory.CreateDirectory(savedDataDirectoryStr);
        }

        // check if playerScoreList is null, if so, load data
        if (playerScoreList == null) {
            //creates a new player score board
            playerScoreList = new List<PlayerScore>();
        }

        // TODO save score to disk
        playerScoreList.Add(ss);
        savedDataObject.playerScores = playerScoreList;
        string json = JsonUtility.ToJson(savedDataObject);
        File.WriteAllText(savedDataDirectoryStr + saveGameData, json);
        Debug.Log("SaveManager - 1");
        // TODO send score data to server
        var postRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3456/scores/add");
        postRequest.ContentType = "application/json";
        postRequest.Method = "POST";
        Debug.Log("SaveManager - 2");
        // build string to send to body
        // write to the http request stream
        using (var streamWriter = new StreamWriter(postRequest.GetRequestStream())) {
            string testID = "6140e1ce698adf17f996fa63"; // TODO replace with username following refactor
            string gameMode = ss.gameMode;
            int thisScore = ss.score;
            ScoreToServer newScore = new ScoreToServer{
                userID = testID,
                gameMode = ss.gameMode,
                points = ss.score
            };
            
            string record = JsonUtility.ToJson(newScore);
            Debug.Log("SaveManager - 3 1234\n" + record);
            streamWriter.Write(record);
        }
        // debug information
        Debug.Log("\nSaveManager - 4");
        var httpResponse =  postRequest.GetResponse(); // dispatch request to server (HttpWebResponse)

        Debug.Log("SaveManager - Exit addScores");
    }

    public void Load()
    {
        Debug.Log("SaveManager - Entering load");
        // retrieve data from past games
        string saveGameDataPath = Application.dataPath + saveDirectory + saveGameData;
        Debug.Log("SaveManager - path: " + saveGameDataPath);
        if (File.Exists(saveGameDataPath))
        {
            string json = File.ReadAllText(saveGameDataPath);
            savedDataObject = JsonUtility.FromJson<SavedDataObject>(json);
            Debug.Log("SaveManager - loaded data");
            playerScoreList = savedDataObject.playerScores;
            foreach (PlayerScore score in playerScoreList) { Debug.Log("SaveManager - " + score.ToString()); }
        }
        else
        {
            Debug.Log("SAVE FILE DOES NOT EXIST");
        }
        // retrieve username
        string userNameDataPath = Application.dataPath + saveDirectory + userNameFile;
        if (File.Exists(userNameDataPath)) {
            userName = System.IO.File.ReadAllText(userNameDataPath);
            Debug.Log("USERNAME: " + userName);
        }
        Debug.Log("SaveManager - Leaving load");
    }

    public List<PlayerScore> GetPlayerScoresList() { return playerScoreList;  }

    public SavedDataObject GetSavedDataObject() { return savedDataObject; }
}

//create a class of playerScores so we can save it
public class SavedDataObject
{
    public List<PlayerScore> playerScores;
}

public class ScoreToServer 
{
    // omitting constructors for unity
    // properties
    public string userID;
    public int points;
    public string gameMode;
}
