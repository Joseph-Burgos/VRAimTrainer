using UnityEngine;
using System;

public static class ScoreManager { 
    public static int m_Score = 0;
    //public static GameObject CurrentScore;

    public static PlayerScore score;

    //void static Start(){
    //    //Debug.Log("Start - enter");
    //    CurrentScore = GameObject.Find("Score");
    //    // initialize scoreboard to 0
    //    m_Score = 0;
    //    CurrentScore.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(m_Score);
    //    //Debug.Log("Start - exit");
    //}

   

    public static void AddToScore(int scoreToBeAdded){
        Debug.Log("AddToScore - enter");
        if (scoreToBeAdded > 0){
            GameObject CurrentScore;
            m_Score += scoreToBeAdded;
            CurrentScore = GameObject.Find("Score");
            CurrentScore.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(m_Score);
        }
        Debug.Log("AddToScore - current score is " + m_Score.ToString());
        Debug.Log("AddToScore - exit");

    }
}
