using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//THIS IS THE SAVE MANAGER. THIS FILE IS MADE TO HANDLE LOADING AND SAVING THE FILE
//game manager must load the save file on awake() so it doesnt overwrite with an empty list
public static class SaveManager 
{
    //make file saveDirectory specifically save into assets folder -- i specifically chose joe folder for testing
    public static string saveDirectory = "/Joe/SaveData/";
    //file name with saved data
    public static string savefileName =  "MyData.txt";
    //create an empty list to add onto
    public static List<PlayerScore> scores = new List<PlayerScore>();
    public static PlayerScoreList playerScoreList = null;

    // Saves a playerscore to disk.
    public static void addScore(PlayerScore ss)
    {
        //get saveDirectory of file
        string dir = Application.dataPath + saveDirectory;
        //check if exists
        if (!Directory.Exists(dir))
        {
            Debug.Log("Save file does not exist, creating");
            Directory.CreateDirectory(dir);
        }

        //creates a new player score board
        PlayerScoreList scoresList = new PlayerScoreList { scores = scores };
        //load old PlayerScoreList
        PlayerScoreList oldscoresList = Load();
   
        //check if old PlayerScoreList empty
        if (oldscoresList != null )
        {
            //store the old PlayerScoreList into the current PlayerScoreList
            scoresList = oldscoresList;
        }
        //add into list
        scoresList.scores.Add(ss);

        //parse save object into json string format
        string json = JsonUtility.ToJson(scoresList);

        //save
        File.WriteAllText(dir + savefileName, json);
    }

    public static void Load()
    {
        // absolute path to data file 
        string absDataPath = Application.dataPath + saveDirectory + savefileName;

        if (File.Exists(absDataPath))
        {
            string json = File.ReadAllText(absDataPath);
            playerScoreList = JsonUtility.FromJson<PlayerScoreList>(json);
        }
        else
        {
            Debug.Log("SAVE FILE DOES NOT EXIST, RETUNRING NULL");
        }
    }

    public static PlayerScoreList GetPlayerScoreList() { return playerScoreList; }
}

//create a class of scores so we can save it
public class PlayerScoreList
{
    public List<PlayerScore> scores;
}
