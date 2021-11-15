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
    public int accuracy;
    public int targetsHit;
    public int totalTargets;
    public int gamesPlayed;

    private void Awake()
    {
        // retrieve necessary components
    }

    void Start()
    {
        //targetManager = otherGameObject.findComponent<TargetManager>();
        Debug.Log("Visual Feedback - start");
        // TEST
        initializeVisualFeedback();
    }

    void initializeVisualFeedback() {
        // calling data functions
        score = GameSystem.GetComponent <ScoreManager>().GetScore();
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
        // targets
    }

   
}