using System;
﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        //loadData();
        
        
    }

    public void initializeVisualFeedback() {
        Debug.Log("Visual Feedback - enter initializeVisualFeedback");
        // call data functions, make necessary calculations
        loadData();
        ScoreManager scoreManager = GameSystem.GetComponent<ScoreManager>();
        score = scoreManager.GetScore();
        int totalShots = scoreManager.GetShots();
       
        targetsHit = scoreManager.GetHits();
        
        if (totalShots > 0)
        {
            accuracy = (float) targetsHit / totalShots;
        }
        else
        {
            accuracy = 0;
        }
        // set each label
        setLabels();
        // draw graphs
        drawGraphs();
        Debug.Log("Visual Feedback - exiting initializeVisualFeedback");
    }

    void setLabels()
    {
        // score
        ScoreLabel.GetComponent<TMPro.TextMeshPro>().text += score.ToString();
        // accuracy
        AccuracyLabel.GetComponent<TMPro.TextMeshPro>().text += accuracy.ToString() + "%";
        // targets
        TargetsHitLabel.GetComponent<TMPro.TextMeshPro>().text += targetsHit.ToString();
        // games played
        GamesPlayedLabel.GetComponent<TMPro.TextMeshPro>().text += gamesPlayed.ToString();
    }

    void drawGraphs()
    {
        Debug.Log("Visual Feedback - enter draw graphs");
        Debug.Log(playerScoreList);
        foreach(var ps in playerScoreList) {
            Debug.Log("SCORE");
        }
        Graph scoreGraph = ScoreGraph.GetComponent<Graph>();
        Graph accuracyGraph = AccuracyGraph.GetComponent<Graph>();
        //Debug.Log(scoreGraph.createGraph);
        Debug.Log("Visual Feedback - draw graphs - calling create graph");
        
        // GET THE ACUTAL DATA FROM SAVE FILES + MOST RECENT GAME
        Vector2[] testNodes = {new Vector2(0, 0), new Vector2(1, 0.2f), new Vector2(2.0f, 0.6f), new Vector2(2.6f, 0.8f)};
        var tupleList = new List<System.Tuple<float, float>> {
            Tuple.Create(0f, 0f),
            Tuple.Create(1f, 0.2f),
            Tuple.Create(2.0f, 0.6f),
            Tuple.Create(2.6f, 0.8f)
        };
        // scoreGraph.createGraph(tupleList); 
         var scores = getScoreHistory();
        // Debug.Log("Visual Feedback - draw graphs - retrieved scores");
        // Debug.Log(scores);
         scoreGraph.createGraph(scores);
        // accuracyGraph.createGraph(getAccuracyHistory());
        Debug.Log("Visual Feedback - exit draw graphs");
    }

    void loadData() {
        SaveManager saveManager = GameSystem.GetComponent<SaveManager>();
        saveManager.Load();
        playerScoreList = saveManager.GetPlayerScoresList();
        // check if the gamesystem exists and has a GameMode enum set
        // if so -> filter the data on the gamemode
        // get the date range (this is the x-axis)
    }

    List<Tuple<float, float>> getScoreHistory() {
        Debug.Log("Visual Feedback - enter getScoreHistory");
        DateTime oldest = playerScoreList.Min(ps => DateTime.Parse(ps.dateTime));
        DateTime today = System.DateTime.Now;
        TimeSpan duration = today - oldest;
        int topScore = playerScoreList.Max(ps => ps.score);
        List<Tuple<float, float>> scoreHistory = new List<Tuple<float, float>>();
        foreach (PlayerScore playerScore in playerScoreList)
        {
            float x = (DateTime.Parse(playerScore.dateTime) - oldest).Minutes; // gets the timeframe in the unit of minutes
            float y = playerScore.score;
            scoreHistory.Add(Tuple.Create(x, y));
        }
        Debug.Log("Visual Feedback - exiting getScoreHistory");
        return scoreHistory;
    }

    List<Tuple<float, float>> getAccuracyHistory()
    {
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