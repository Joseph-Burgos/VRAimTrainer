using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using System.Threading.Tasks;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private HttpClient client; // FIXME should this be 'static' and 'readonly'???

    private async Task AwakeAsync()
    {
        Debug.Log("Starting the leaderboard retrieval process");
        client = new HttpClient();
        string responseString = await client.GetStringAsync("http://localhost:3456/scores");

        //HttpResponseMessage response = await client.GetAsync("http://www.contoso.com/");
        //response.EnsureSuccessStatusCode();
        //string responseBody = await response.Content.ReadAsStringAsync();
        Debug.Log(responseString);
        // asdf 
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
