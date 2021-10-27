using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    private int m_Score;
    private int m_Shots;
    private GameObject CurrentScore;

    void Start(){
        //Debug.Log("Start - enter");
        CurrentScore = GameObject.Find("Score");
        // initialize scoreboard to 0
        m_Score = 0;
        m_Misses = 0;
        CurrentScore.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(m_Score);
        Debug.Log("AddToScore - current START score is " + CurrentScore);
        //Debug.Log("Start - exit");
    }

   

    public void AddToScore(int scoreToBeAdded){
        CurrentScore = GameObject.Find("Score");
        //Debug.Log("AddToScore - enter");
        if (scoreToBeAdded > 0){
            m_Score += scoreToBeAdded;
            CurrentScore.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(m_Score);
        }
        Debug.Log("AddToScore - current  OBJECT score is " + CurrentScore);
        Debug.Log("AddToScore - current score is " + m_Score.ToString());
        Debug.Log("AddToScore - exit");
    }

    public void AddToShots() {
        m_Shots++;
    }
}
