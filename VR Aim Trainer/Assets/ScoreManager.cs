using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    private int m_Score;
    private int m_Shots;
    private int m_Hits;
    private GameObject CurrentScore = null;

    void Start(){
        //Debug.Log("Start - enter");
        CurrentScore = GameObject.Find("Score");
        
        if (!CurrentScore){
            return;
        }
        
        // initialize scoreboard to 0
        m_Score = 0;
        m_Shots = 0;
        m_Hits = 0;
        CurrentScore.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(m_Score);
    }

   public int GetScore(){
       return m_Score;
   }

    public void AddToScore(int scoreToBeAdded){
        m_Hits = m_Hits + 1;
        CurrentScore = GameObject.Find("Score");
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

    public int GetShots() { return m_Shots; }

    public int GetHits() { return m_Hits; }
}
