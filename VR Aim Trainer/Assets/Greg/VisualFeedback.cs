using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class defines the behavior of the Visual Feedback prefab.
// This prefab is designed to be displayed to user after a game to apprise them 
// of their progress within the game.
public class VisualFeedback : MonoBehaviour {
    // objects to interact with
    [SerializeField] GameObject GameSystem;

    // labels
    [SerializeField] GameObject ScoreLabel;
    [SerializeField] GameObject AccuracyLabel;
    [SerializeField] GameObject TargetsHitLabel;
    [SerializeField] GameObject GamesPlayedLabel;

    // graphs
    [SerializeField] GameObject ScoreGraph;
    [SerializeField] GameObject AccuracyGraph;

    // data members
    public int score;
    public float accuracy;
    public int targetsHit;
    public int totalTargets;
    public int gamesPlayed;

    // saved data
    private List<PlayerScore> playerScoreList;

    

    private void Awake()
    {
        // initial members to zero
        score = 0;
        accuracy = 0;
        targetsHit = 0;
        gamesPlayed = 0;
        playerScoreList = null;
    }

    void Start()
    {
        //targetManager = otherGameObject.findComponent<TargetManager>();
        Debug.Log("Visual Feedback - start");
        // Load data for the players history from the SaveManager
        loadData();
        // ScoreManager scoreManager = GameSystem.GetComponent<ScoreManager>();
        Debug.Log("Visual Feedback - exit start");
    }

    public void initializeVisualFeedback(int newscore, float acc) {
        Debug.Log("Visual Feedback - enter initializeVisualFeedback");
        // call data functions, make necessary calculations
        // loadData();
        // ScoreManager scoreManager = GameSystem.GetComponent<ScoreManager>();
        Debug.Log("Visual Feedback - got scoremanager");
        score = newscore;
        accuracy = acc;
        // set each label in menu
        setLabels();
        // draw graphs on menu
        drawGraphs();
        
        Debug.Log("Visual Feedback - exiting initializeVisualFeedback");
    }

    // This function sets the labels on the menu.
    void setLabels()
    {
        Debug.Log("Visual Feedback - enter setLabels");
        // score
        ScoreLabel.GetComponent<TMPro.TextMeshPro>().text += score.ToString();
        // accuracy
        AccuracyLabel.GetComponent<TMPro.TextMeshPro>().text += accuracy.ToString() + "%";
        // targets
        TargetsHitLabel.GetComponent<TMPro.TextMeshPro>().text += targetsHit.ToString();
        // games played
        GamesPlayedLabel.GetComponent<TMPro.TextMeshPro>().text += gamesPlayed.ToString();
        Debug.Log("Visual Feedback - exit setLabels");
    }

    // This function draws graphs on the menu.
    void drawGraphs()
    {
        Debug.Log("Visual Feedback - enter draw graphs");
        Graph scoreGraph = ScoreGraph.GetComponent<Graph>();
        Graph accuracyGraph = AccuracyGraph.GetComponent<Graph>();
        // Draw the scores graph
         var scores = getScoreHistory();
         scoreGraph.createGraph(scores, false);
         accuracyGraph.createGraph(getAccuracyHistory(), true);
        Debug.Log("Visual Feedback - exit draw graphs");
    }

    // This function loads data form the SvaeManager
    void loadData() {
        Debug.Log("Visual Feedback - enter load");
        SaveManager saveManager = GameSystem.GetComponent<SaveManager>();
        saveManager.Load();
        playerScoreList = saveManager.GetPlayerScoresList();
        gamesPlayed = playerScoreList.Count + 1;
        // check if the gamesystem exists and has a GameMode enum set
        // if so -> filter the data on the gamemode
        // get the date range (this is the x-axis)
        Debug.Log("Visual Feedback - exit load");
    }

    // This function orders the player history by date and extracts data to represent each played game as a node on the graphs.
    List<Tuple<float, float>> getScoreHistory() {
        // Debug.Log("Visual Feedback - enter getScoreHistory");
        // DateTime defines the X Axis of each graph
        // All X values will be relative to the length of time the player has played the game.
        DateTime oldest = playerScoreList.Min(ps => DateTime.Parse(ps.dateTime)); // x origin
        DateTime today = System.DateTime.Now; // x extrema
        TimeSpan duration = today - oldest;
        //int topScore = playerScoreList.Max(ps => ps.score);
        List<Tuple<float, float>> scoreHistory = new List<Tuple<float, float>>();
        foreach (PlayerScore playerScore in playerScoreList)
        {
            float x = (DateTime.Parse(playerScore.dateTime) - oldest).Minutes; // gets the timeframe in the unit of minutes
            float y = playerScore.score; 
            scoreHistory.Add(Tuple.Create(x, y));
        }
        // Debug.Log("Visual Feedback - exiting getScoreHistory");
        return scoreHistory;
    }

    // This function orders the player history by date and extracts data to represent each played game as a node on the graphs.
    List<Tuple<float, float>> getAccuracyHistory()
    {
        // DateTime defines the X Axis of each graph
        // All X values will be relative to the length of time the player has played the game.
        DateTime oldest = playerScoreList.Min(ps => DateTime.Parse(ps.dateTime));
        DateTime today = System.DateTime.Now;
        TimeSpan duration = today - oldest;
        float xMax = duration.Minutes;
        List<Tuple<float, float>> accuracyHistory = new List<Tuple<float, float>>();
        foreach (PlayerScore playerScore in playerScoreList) {
            float x = (DateTime.Parse(playerScore.dateTime) - oldest).Minutes;
            float y = playerScore.accuracy;
            accuracyHistory.Add(Tuple.Create(x, y));
        }
        return accuracyHistory;
    }


}