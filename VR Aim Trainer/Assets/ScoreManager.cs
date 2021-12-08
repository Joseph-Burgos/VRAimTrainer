using UnityEngine;
using System;

// This class is used to keep track of the players score in any given game.
// It also interfaces with the 'score' display in order to update that.
public class ScoreManager : MonoBehaviour
{
    private int m_Score;
    private int m_Shots;
    private int m_Hits;
    [SerializeField]
    private GameObject CurrentScore = null;

    void Start(){
        // Debug.Log("Start - enter");
        // Find the current 'score' display, if it exists
        // Otherwise, fail silently.
        CurrentScore = GameObject.Find("Score");
<<<<<<< HEAD
=======

>>>>>>> c22c1414c58dd36f8adba9bbdaaced4750355822
        
        if (!CurrentScore){
            return;
        }
        
        // initialize score to 0
        m_Score = 0;
        m_Shots = 0;
        m_Hits = 0;
        // initialize the display
        CurrentScore.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(m_Score);
    }

    public int GetScore(){
        return m_Score;
    }

    // Increment the current score by any given positive amount.
    public void AddToScore(int scoreToBeAdded){
        m_Hits = m_Hits + 1;
        CurrentScore = GameObject.Find("Score");
        if (scoreToBeAdded > 0){
            m_Score += scoreToBeAdded;
            CurrentScore.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(m_Score);
        }
        // Debug.Log("AddToScore - current  OBJECT score is " + CurrentScore);
        // Debug.Log("AddToScore - current score is " + m_Score.ToString());
        // Debug.Log("AddToScore - exit");
    }

    // Set the current score to any given positive value.
    // Update the score display with to reflect this change.
    public void SetScore(int score){
        CurrentScore = GameObject.Find("Score");
        if (score > 0){
            m_Score = score;
            CurrentScore.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(m_Score);
        }
    }

    // Increment shots counter.
    public void AddToShots() {
        m_Shots++;
    }

    public int GetShots() { return m_Shots; }

    public int GetHits() { return m_Hits; }
}
