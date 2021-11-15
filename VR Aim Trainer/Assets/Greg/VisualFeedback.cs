using System;
﻿using System.Collections;
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

    private void Awake()
    {
        score = 0;
        accuracy = 0;
        targetsHit = 0;
        gamesPlayed = 0;
    }

    void Start()
    {
        //targetManager = otherGameObject.findComponent<TargetManager>();
        Debug.Log("Visual Feedback - start");
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
        Vector2[] testNodes = { new Vector2(0, 0), new Vector2(1, 0.2f), new Vector2(2.0f, 0.6f), new Vector2(2.6f, 0.8f) }; // TEST DATA
        // GET THE ACUTAL DATA FROM SAVE FILES + MOST RECENT GAME
        scoreGraph.createGraph(testNodes);
        accuracyGraph.createGraph(testNodes);
        //Debug.Log("Visual Feedback - exit draw graphs");
    }

   
}