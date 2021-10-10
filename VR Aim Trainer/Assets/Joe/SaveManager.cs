using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//THIS IS THE SAVE MANAGER. THIS FILE IS MADE TO HANDLE LOADING AND SAVING THE FILE
//game manager must load the save file on awake() so it doesnt overwrite with an empty list
public static class SaveManager 
{
    //make file directory specifically save into assets folder -- i specifically chose joe folder for testing
    public static string directory = "/Joe/SaveData/";
    //file name
    public static string fileName =  "MyData.txt";
    //create an empty list to add onto
    public static List<PlayerScore> scores = new List<PlayerScore>();

    // add a given score to the list of files
    public static void addScore(PlayerScore ss)
    {
        //get directory of file
        string dir = Application.dataPath + directory;
        //check if exists
        if (!Directory.Exists(dir))
        {
            Debug.Log("Save file does not exist, creating");
            Directory.CreateDirectory(dir);
        }
        //creates a new player score board
        scoreboard sb = new scoreboard { scores = scores };
        //load old scoreboard
        scoreboard oldSB = Load();
        //check if old scoreboard empty
        if (oldSB != null )
        {
            //store the old scoreboard into the current scoreboard
            sb = oldSB;
        }
        //add into list
        sb.scores.Add(ss);
        //parse save object into json string format
        string json = JsonUtility.ToJson(sb);
        //save
        File.WriteAllText(dir + fileName, json);
    }

    public static scoreboard Load()
    {
        //get file path 
        string fullPath = Application.dataPath + directory + fileName;
        //creates a new player score board
        scoreboard sb = new scoreboard();
        //PlayerScore ss = new PlayerScore();
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            sb = JsonUtility.FromJson<scoreboard>(json);
        }
        else
        {
            Debug.Log("SAVE FILE DOES NOT EXIST, RETUNRING NULL");
            return null;
        }
        return sb;
    }

    public static void saveScore (int score, string name, int time, string mode) {
        PlayerScore newScore = new PlayerScore 
        {
            gameMode = mode;
            score = score;
            time = time;
            userName = name;
        }
        // TODO write to disk
        // TODO send scores to backend server
    }    
}

//create a class of scores so we can save it
public class scoreboard
{
    public List<PlayerScore> scores;
}
