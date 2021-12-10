using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

// This class defines the behaviour for the leaderboard table prefab.
public class HighscoreTable : MonoBehaviour
{
    // transforms that will be used to render each record on the leaderboard
    private Transform entryContainer; 
    private Transform entryTemplate;
    // http client to request scores from server
    private HttpClient client;
    // instance of 'scoreboard' class
    // used to parse json from server into a list of 'score' objects
    private Scoreboard sb;

    private async Task Awake()
    {
        // request scores from server
        client = new HttpClient();
        string responseString = await client.GetStringAsync("https://vr-aim-trainer.herokuapp.com/scores?topScores=5"); 
        // insert received data into a json string
        // this is a work around to adapt Unity to communicate with a Node.js server
        string formattedResponse = "{\"scores\":" + responseString + "}";
        // load resultant list into the scoreboard object
        sb = JsonUtility.FromJson<Scoreboard>(formattedResponse);
        // extract the scores list from the scoreboard
        List<Score> highScores = sb.scores; // leaderboard scores

        // bind gameobjects that comprise the leaderboard prefab
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        // define offsets to position each element iteratively
        float templateX = -0.6f;
        float templateZ = 0.5f;
        float templateY = -0.2f;
        float Yinterval = -0.5f;
        // create each record and insert it into the game space
        for(int i = 0; i < highScores.Count; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            // position current element
            entryRectTransform.anchoredPosition3D = new Vector3(templateX, templateY, templateZ);
            templateY = templateY + Yinterval; // update Y position of next element
            
            entryTransform.gameObject.SetActive(true);
            // write rank strings
            int rank = i + 1;
            string rankString;
            switch (rank)
            {
                default: rankString = rank + "th"; break;
                case 1: rankString = "1st"; break;
                case 2: rankString = "2nd"; break;
                case 3: rankString = "3rd"; break;
            }

            Score currentScore = highScores[i];
            // set text fields
            int score = currentScore.points;
            entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();
            string name = currentScore.userName;
            entryTransform.Find("nameText").GetComponent<Text>().text = name;
            // get date string, parse to datetime, and extract the mm/dd/yyyy
            DateTime scoreDateTime = DateTime.Parse(currentScore.date);
            string dateString = scoreDateTime.Month + "/" + scoreDateTime.Day + "/" + scoreDateTime.Year;
            entryTransform.Find("timeText").GetComponent<Text>().text = dateString;
        }
    }
}

// This class is used to load a list of score objects. 
// These are received from the server in json format.
public class Scoreboard
{
    public List<Score> scores;

    public override string ToString()
    {
        string sbout = "";
        foreach (Score score in scores) {
            string scorerep = $"{score.userName} {score.gameMode} {score.points}\n";
            sbout = sbout + scorerep;
        }
        return sbout;
    }
}

// This class is used to load json for individual score records
[System.Serializable]
public class Score {
    public string userName;
    public string gameMode;
    public int points;
    public string date;
}