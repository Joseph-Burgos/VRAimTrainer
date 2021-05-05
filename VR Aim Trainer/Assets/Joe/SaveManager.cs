using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//THIS IS THE SAVE MANAGER. THIS FILE IS MADE TO HANDLE LOADING AND SAVING THE FILE
public static class SaveManager 
{
    public static string directory = "/SaveData/";
    public static string fileName =  "MyData.txt";
    public static List<PlayerScore> scores = new List<PlayerScore>();

    // add a given score to the lsit of files
    public static void addScore(PlayerScore ss)
    {
        //get directory of file
        string dir = Application.persistentDataPath + directory;
        //check if exists
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        //add into list
        scores.Add(ss);
        //create a scoreboard object to save
        scoreboard sb = new scoreboard { scores = scores };
        //parse save object into json string format
        string json = JsonUtility.ToJson(sb);

        //save
        File.WriteAllText(dir + fileName, json);
    }

    public static scoreboard Load()
    {
        //get file path
        string fullPath = Application.persistentDataPath + directory + fileName;
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
            Debug.Log("Save file does not exist");
        }
        
        return sb;

    }
}

//create a class ofd scores so we can save it
public class scoreboard
{
    public List<PlayerScore> scores;
}
