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
    public double accuracy;
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
        loadData();
        // TEST
        gamesPlayed = 1000000000;
        initializeVisualFeedback();
    }

    void initializeVisualFeedback() {
        // call data functions, make necessary calculations
        ScoreManager scoreManager = GameSystem.GetComponent<ScoreManager>();
        score = scoreManager.GetScore();
        int totalShots = scoreManager.GetShots();
       
        targetsHit = scoreManager.GetHits();
        
        if (totalShots > 0)
        {
            accuracy = (double) targetsHit / totalShots;
        }
        else
        {
            accuracy = 0;
        }
        // set each label
        setLabels();
        // draw graphs
        drawGraphs();
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
        //Debug.Log("Visual Feedback - enter draw graphs");
        Graph scoreGraph = ScoreGraph.GetComponent<Graph>();
        Graph accuracyGraph = AccuracyGraph.GetComponent<Graph>();
        
        // GET THE ACUTAL DATA FROM SAVE FILES + MOST RECENT GAME
        scoreGraph.createGraph(getScoreHistory());
        accuracyGraph.createGraph(getAccuracyHistory());
        //Debug.Log("Visual Feedback - exit draw graphs");
    }

    void loadData() {
        SaveManager saveManager = GameSystem.GetComponent<SaveManager>();
        List<PlayerScore> playerScoreList = saveManager.GetPlayerScoresList();
        // check if the gamesystem exists and has a GameMode enum set
        // if so -> filter the data on the gamemode
        // get the date range (this is the x-axis)
    }

    List<Tuple<float, float>> getScoreHistory() {
        DateTime oldest = playerScoreList.Min(ps => ps.dateTime);
        DateTime today = System.DateTime.Now;
        TimeSpan duration = today - oldest;
        int topScore = playerScoreList.Max(ps => ps.score);
        List<Tuple<float, float>> scoreHistory = new List<Tuple<float, float>>();
        //Vector2[] scoreHistory = playerScoreList.Select(playerScore => {
        //    //int normalizedScore = 
        //    new Vector2(playerScore.score, playerScore.dateTime);
        //}).Cast<Vector2>().ToArray();
        //Vector2[] testNodes = { new Vector2(0, 0), new Vector2(1, 0.2f), new Vector2(2.0f, 0.6f), new Vector2(2.6f, 0.8f) }; // TEST DATA
        foreach (PlayerScore playerScore in playerScoreList)
        {
            float x = (playerScore.dateTime - oldest).Minutes;
            float y = playerScore.score;
            scoreHistory.Add(Tuple.Create(x, y));
        }
        return scoreHistory;
    }

    List<Tuple<float, float>> getAccuracyHistory()
    {
        DateTime oldest = playerScoreList.Min(ps => ps.dateTime);
        DateTime today = System.DateTime.Now;
        TimeSpan duration = today - oldest;
        float xMax = duration.Minutes;
        List<Tuple<float, float>> accuracyHistory = new List<Tuple<float, float>>();
        //= playerScoreList.Select(playerScore =>
        //{
        //    float x = (playerScore.dateTime - oldest).Minutes;
        //    float y = playerScore.accuracy;

        //    return Tuple.Create(x, y);
        //});
        //
        foreach (PlayerScore playerScore in playerScoreList) {
            float x = (playerScore.dateTime - oldest).Minutes;
            float y = playerScore.accuracy;
            accuracyHistory.Add(Tuple.Create(x, y));
        }
        return accuracyHistory;
    }


}