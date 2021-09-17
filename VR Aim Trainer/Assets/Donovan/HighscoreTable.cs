using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
//using System.Web.Script.Serialization;
public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private HttpClient client; // FIXME should this be 'static' and 'readonly'???
    private Scoreboard sb;

    private async Task Awake()
    {
        Debug.Log("Starting the leaderboard retrieval process");
        client = new HttpClient();
        string responseString = await client.GetStringAsync("http://localhost:3456/scores?topScores=5"); // TODO handle no connection

        Debug.Log("original response");
        Debug.Log(responseString);
        string formattedResponse = "{\"scores\":" + responseString + "}";
        Debug.Log("formatted response");
        Debug.Log(formattedResponse);
        sb = JsonUtility.FromJson<Scoreboard>(formattedResponse);
        Debug.Log("Retrieved objects from server successfully");
        Debug.Log(sb.ToString());

        List<Score> highScores = sb.scores;

        
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        
        float templateHeight = 30f;
        for(int i = 0; i < highScores.Count; i++)
        {
            
            
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            Debug.Log(entryTransform);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            Debug.Log("about to get a new vector to move");
            Debug.Log(entryRectTransform);
            entryRectTransform.anchoredPosition3D = new Vector3(0f, -templateHeight * i, 0f);
            Debug.Log("the vector seemed to work");
            entryTransform.gameObject.SetActive(true);
            Debug.Log(string.Format("Successfully loaded the leaderboard {0}", i));

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
            entryTransform.Find("timeText").GetComponent<Text>().text = "12:34";
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