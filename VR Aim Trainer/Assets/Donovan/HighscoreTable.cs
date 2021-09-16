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
        string responseString = await client.GetStringAsync("http://localhost:3456/scores?topScores=2");

        Debug.Log("original response");
        Debug.Log(responseString);
        string formattedResponse = "{\"scores\":" + responseString + "}";
        Debug.Log("formatted response");
        Debug.Log(formattedResponse);
        sb = JsonUtility.FromJson<Scoreboard>(formattedResponse);
        Debug.Log("Retrieved objects from server successfully");
        Debug.Log(sb.ToString());

        Debug.Log("Successfully loaded the leaderboard");
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 20f;
        for(int i = 0; i < 10; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            string rankString;
            switch (rank)
            {
                default: rankString = rank + "TH"; break;
                case 1: rankString = "1st"; break;
                case 2: rankString = "2nd"; break;
                case 3: rankString = "3rd"; break;
            }

            entryTransform.Find("postText").GetComponent<Text>().text = rankString;
            int score = Random.Range(0, 10000);
            entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();
            string name = "AAA";
            entryTransform.Find("nameText").GetComponent<Text>().text = name;
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
    public string points;
    public string date;
}