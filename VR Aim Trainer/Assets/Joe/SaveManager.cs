using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Web;
using UnityEngine;
using System.IO;

// This class is performs operations that backs up data from played games.
public class SaveManager : MonoBehaviour
{
    // directory for saving data 
    public string saveDirectory = "/Joe/SaveData/";
    // name of file to store data
    public string saveGameData =  "MyData.txt";
    // name of file with player's username
    public string userNameFile = "Username.txt";
    // current username
    public string userName = "Default";
    // most recently loaded SavedDataObject
    public SavedDataObject savedDataObject = null;
    // current list of playerScores
    public List<PlayerScore> playerScoreList = null;

    // Upon loading of class, start loading data from disk.
    public void Awake() { Load(); }

    // Saves a playerscore to disk.
    public void addScore(PlayerScore ss)
    {
        // Debug.Log("SaveManager - Enter addScores");

        //get saveDirectory of file
        string savedDataDirectoryStr = Application.dataPath + saveDirectory;
        //check if exists
        if (!Directory.Exists(savedDataDirectoryStr))
        {
            // Debug.Log("Save file does not exist, creating");
            Directory.CreateDirectory(savedDataDirectoryStr);
        }

        // check if playerScoreList is null, if so, load data
        if (playerScoreList == null) {
            //creates a new player score board
            playerScoreList = new List<PlayerScore>();
        }

        // save score to disk
        playerScoreList.Add(ss);
        savedDataObject.playerScores = playerScoreList;
        string json = JsonUtility.ToJson(savedDataObject);
        File.WriteAllText(savedDataDirectoryStr + saveGameData, json);
        // Debug.Log("SaveManager - 1");
        // send score data to server
        var postRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3456/scores/add");
        postRequest.ContentType = "application/json";
        postRequest.Method = "POST";
        // Debug.Log("SaveManager - 2");
        // build string to send data in request body, write to the http request stream
        using (var streamWriter = new StreamWriter(postRequest.GetRequestStream())) {
            string gameMode = ss.gameMode;
            int thisScore = ss.score;
            ScoreToServer newScore = new ScoreToServer{
                userID = userName,
                gameMode = ss.gameMode,
                points = ss.score
            };
            string record = JsonUtility.ToJson(newScore);
            streamWriter.Write(record);
        }
        // debug information
        // Debug.Log("\nSaveManager - 4");
        var httpResponse =  postRequest.GetResponse(); // dispatch request to server, ignore response

        // Debug.Log("SaveManager - Exit addScores");
    }

    // Loads data from disk.
    public void Load()
    {
        // Debug.Log("SaveManager - Entering load");
        // constructs absolute path to data file
        string saveGameDataPath = Application.dataPath + saveDirectory + saveGameData;
        // if file exists, proceed to load, otherwise open a new file
        if (File.Exists(saveGameDataPath))
        {
            string json = File.ReadAllText(saveGameDataPath);
            savedDataObject = JsonUtility.FromJson<SavedDataObject>(json);
            // Debug.Log("SaveManager - loaded data");
            playerScoreList = savedDataObject.playerScores;
            // foreach (PlayerScore score in playerScoreList) { Debug.Log("SaveManager - " + score.ToString()); }
        }
        else
        {
            // Debug.Log("SAVE FILE DOES NOT EXIST");
        }
        // retrieve username, if one exists, otherwise will default to "default"
        string userNameDataPath = Application.dataPath + saveDirectory + userNameFile;
        if (File.Exists(userNameDataPath)) {
            userName = System.IO.File.ReadAllText(userNameDataPath);
            // Debug.Log("USERNAME: " + userName);
        }
        // Debug.Log("SaveManager - Leaving load");
    }

    public List<PlayerScore> GetPlayerScoresList() { return playerScoreList;  }

    public SavedDataObject GetSavedDataObject() { return savedDataObject; }
}

// This class is used to load the saved data file from a json format.
public class SavedDataObject
{
    public List<PlayerScore> playerScores;
}

// This class is used to write relevant game data into a json format.
public class ScoreToServer 
{
    public string userID; // user name
    public int points; // score
    public string gameMode;
}
