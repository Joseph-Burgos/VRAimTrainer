using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//THIS IS THE SAVE MANAGER. THIS FILE IS MADE TO HANDLE LOADING AND SAVING THE FILE
//game manager must load the save file on awake() so it doesnt overwrite with an empty list
public static class SaveManager 
{
    //make file directory specifically save into assets folder -- i specifically chose joe folder for testing
    public static string directory = "/SaveData/";
    //file name
    public static string fileName =  "SavedData.txt";
    // history of each score on machine
    public static List<PlayerScore> scoreHistory = new List<PlayerScore>();
    // API endpoint
    private static APIendpoint = "https://localhost:9000/score/add";

    // add a given score to the list of files
    public static void RetrieveAndAddScore(PlayerScore ss)
    {
        //get directory of file
        string dir = Application.dataPath + directory;
        //check if exists, if not create it
        if (!Directory.Exists(dir))
        {
            Debug.Log("Save file does not exist, creating");
            Directory.CreateDirectory(dir);
        }
        //creates a new player score board
        Scoreboard sb = new Scoreboard { scores = scores };
        //load old scoreboard
        Scoreboard oldSB = Load();
        //check if old scoreboard empty
        if (oldSB != null )
        {
            //store the old scoreboard into the current scoreboard
            sb = oldSB;
        }
        //add into list
        sb.scores.Add(ss);
        scores = sb.scores;
        //parse save object into json string format
        string json = JsonUtility.ToJson(sb);
        //save back to disk
        File.WriteAllText(dir + fileName, json);
    }

    public static scoreboard LoadScores()
    {
        //get file path 
        string fullPath = Application.dataPath + directory + fileName;
        //creates a new player score board
        Scoreboard sb = new Scoreboard();
        //PlayerScore ss = new PlayerScore();
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            sb = JsonUtility.FromJson<scoreboard>(json);
        }
        else
        {
            Debug.Log("SAVE FILE DOES NOT EXIST, RETURNING NULL");
            return null;
        }
        return sb;
    }

    public static List<PlayerScore> getScoreHistory() { return scores; }

    public static void sendScore(PlayerScore newPlayerScore) {
        client = new HttpClient();
        string request = API + "?" + "user=" + newPlayerScore.userName + "&score=" + 
            newPlayerScore.score + "&time=" + newPlayerScore.time + "&mode=" + newPlayerScore.gameMode;
        string responseString = await client.GetStringAsync(); // TODO handle no connection
        if (!responseString) {
            Debug.log("An error occurred contacting server. Scores were not saved.")
        }

    }

    public static void saveScore (int score, string name, int time, string mode) {
        PlayerScore newScore = new PlayerScore 
        {
            gameMode = mode;
            score = score;
            time = time;
            userName = name;
        }
        RetrieveAndAddScore(newScore); // write to disk
        sendScore(newScore);
    }    
}

//create a class of scores so we can save it
public class Scoreboard
{
    public List<PlayerScore> scores;
}
