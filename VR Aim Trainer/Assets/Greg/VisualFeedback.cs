using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualFeedback : MonoBehaviour {
    public GameObject otherGameObject;
    private List<PlayerScore> scores; // history of past games
    private int lastScore;
    private TargetManager targetManager;
    private List<Target> targets;
    private ScoreManager scoreManager;
    private int hits;
    private int misses;

    void Start()
    {
        //targetManager = otherGameObject.findComponent<TargetManager>();
        Debug.Log("Visual Feedback - start");
    }

    //public void calculatePlaytimeHistory()
    //{
    //scores = SaveManager.getScores();
    //targets = targetManager.getTargets();
    //lastScore = scoreManager.getScore();
    //hits = scoreManager.getHits();
    //misses = ScoreManager.getMisses();
    //}

    // Metrics
    // Method that generates a list of x, y points for a graph displaying scores over time, by game mode
    //public List<Tuple<int, int>> generateScoreData()
    //{
    //    var scoreDataContainer = new List<(int, int)>();
    //    for (int i = 0; i<scores.length; i++) {
    //        scoreDataContainer.Add(Tuple.Create(scores[i].score, scores[i].date));
    //    }
    //    return scoreDataContainer; 
    //}

    // Method that generates a list of x, y points for a graph displaying accuracy scores over the length of time
    //public List<Tuple<float, int>> generateAccuracyData()
    //{
    //    var accDataContainer = new List<(float, int)>();
    //    for (int i = 0; i < scores.length; i++)
    //    {
    //        accDataContainer.Add(Tuple.Create(scores[i].accuracy, scores[i].date));
    //    }
    //    return accDataContainer; 
    // }

    // Total games played
    //public int getTotalGamesPlayed() { return scores.length; }

    // Avg lifetime of target in this last game
    //public float getAverageLifetime()
    //{
    //    float totalLifetime = 0.0;
    //    for (int i = 0; i < targets.length; i++)
    //    {
    //        totalLifetime = totalLifetime + targets[i].lifetime;
    //    }
    //    float avgLifetime = totalLifetime / targets.length;
    //    return avgLifetime;
    //}

    // Avg lifetime of target over all games
    //public float getOverallAverageLifetime()
    //{
    //    float recAvgLifetime = 0.0;
    //    for (int i = 0; i < scores.length; i++)
    //    {
    //        recAvgLifetime = recAvgLifetime + scores[i].lifetime;
    //    }
    //    float overallAvgLifetime = recAvgLifetime / scores.length;
    //    return overallAvgLifetime;
    //}

    // Calculate accuracy for this game
    //public float getAccuracy()
    //{
    //    float accuracy = hits / misses;
    //    return accuracy;
    //}

    // Calculate accuracy over all games
    //public float getOverallAccuracy()
    //{
    //    float totalOverallAccuracy = 0.0;
    //    for (int i = 0; i < scores.length; i++)
    //    {
    //        totalOverallAccuracy = totalOverallAccuracy + scores[i].lifetime;
    //    }
    //    float overallAccuracy = totalOverallAccuracy / scores.length;
    //    return overallAccuracy;
    //}
}