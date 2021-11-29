using System.Collections;
using System.Collections.Generic;
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

    public void Awake()
    {
        Load();    
    }


    // Saves a playerscore to disk.
    public void addScore(PlayerScore ss)
    {
        Debug.Log("SaveManager - Enter addScores");

        Debug.Log("SaveManager - Exit addScores");
        // //get saveDirectory of file
        // string dir = Application.dataPath + saveDirectory;
        // //check if exists
        // if (!Directory.Exists(dir))
        // {
        //     Debug.Log("Save file does not exist, creating");
        //     Directory.CreateDirectory(dir);
        // }

        // //creates a new player score board
        // SavedDataObject playerScoresList = new SavedDataObject { playerScores = playerScores };
        // //load old SavedDataObject
        // SavedDataObject oldplayerScoresList = Load();

        // //check if old SavedDataObject empty
        // if (oldplayerScoresList != null )
        // {
        //     //store the old SavedDataObject into the current SavedDataObject
        //     playerScoresList = oldplayerScoresList;
        // }
        // //add into list
        // playerScoresList.playerScores.Add(ss);

        // //parse save object into json string format
        // string json = JsonUtility.ToJson(playerScoresList);

        // //save
        // File.WriteAllText(dir + saveGameData, json);
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
