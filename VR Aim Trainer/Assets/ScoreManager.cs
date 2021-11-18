using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    private int m_Score;
    private GameObject CurrentScore = null;

    void Start(){
        //Debug.Log("Start - enter");
        CurrentScore = GameObject.Find("Score");
        
        if (!CurrentScore){
            return;
        }
        
        // initialize scoreboard to 0
        m_Score = 0;
        CurrentScore.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(m_Score);
    }

   public int GetScore(){
       return m_Score;
   }

    public void AddToScore(int scoreToBeAdded){
        CurrentScore = GameObject.Find("Score");
        if (scoreToBeAdded > 0){
            m_Score += scoreToBeAdded;
            CurrentScore.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(m_Score);
        }
    }
}
