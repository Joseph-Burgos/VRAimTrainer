using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private HttpClient client;
    private Scoreboard sb;

    private async Task Awake()
    {
        // Debug.Log("Starting the leaderboard retrieval process");
        client = new HttpClient();
        string responseString = await client.GetStringAsync("http://localhost:3456/scores?topScores=5"); 

        string formattedResponse = "{\"scores\":" + responseString + "}";
        sb = JsonUtility.FromJson<Scoreboard>(formattedResponse);

        List<Score> highScores = sb.scores;

        
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        float templateX = -0.6f;
        float templateZ = 0.5f;
        float templateY = -0.2f;
        float Yinterval = -0.5f;

        for(int i = 0; i < highScores.Count; i++)
        {
            
            
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            // Debug.Log(entryTransform);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            // Debug.Log("about to get a new vector to move");
            // Debug.Log(entryRectTransform);
            
            entryRectTransform.anchoredPosition3D = new Vector3(templateX, templateY, templateZ);
            templateY = templateY + Yinterval;
            // Debug.Log("the vector seemed to work");
            entryTransform.gameObject.SetActive(true);
            // Debug.Log(string.Format("Successfully loaded the leaderboard {0}", i));

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

            //entryTransform.Find("postText").GetComponent<Text>().text = rankString;
            int score = currentScore.points;
            entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();
            //string mode = currentScore.gameMode;
            //entryTransform.Find("gameText").GetComponent<Text>().text = mode.ToString();
            string name = currentScore.userName;
            entryTransform.Find("nameText").GetComponent<Text>().text = name;
            // TODO get date string, parse to datetime, and extract the mm/dd/yyyy
            DateTime scoreDateTime = DateTime.Parse(currentScore.date);
            string dateString = scoreDateTime.Month + "/" + scoreDateTime.Day + "/" + scoreDateTime.Year;
            entryTransform.Find("timeText").GetComponent<Text>().text = dateString;
        }
    }
}

//create a class of scores so we can save it
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

[System.Serializable]
public class Score {
    public string userName;
    public string gameMode;
    public int points;
    public string date;
}