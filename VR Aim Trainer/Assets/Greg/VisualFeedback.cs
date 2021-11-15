using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualFeedback : MonoBehaviour {
    // objects to interact with
    [SerializeField] GameObject GameSystem;
    [SerializeField] GameObject TargetManager;

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
        // calling data functions
        ScoreManager scoreManager = GameSystem.GetComponent<ScoreManager>();
        score = scoreManager.GetScore();
        int totalShots = scoreManager.GetShots();
        Debug.Log("Visual Feedback - totalShots " + totalShots.ToString());
        targetsHit = scoreManager.GetHits();
        Debug.Log("Visual Feedback - hits " + targetsHit.ToString());
        if (totalShots > 0)
        {
            accuracy = (double) targetsHit / totalShots;
        }
        else
        {
            accuracy = 0;
        }
        
        // make necessary calculations
        // set each label
        setLabels();
        // draw graphs
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

   
}