
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    private int m_Score;
    private int num_calls;
    private GameObject CurrentScore;

    void Start(){
        Debug.Log("Start - enter");
        CurrentScore = GameObject.Find("Score");
        num_calls = 2;
        // initialize scoreboard to 0
        m_Score = 0;
        CurrentScore.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(m_Score);
        Debug.Log("Start - exit");
    }

    // Update tests the AddToScore function
    private void Update() 
    {
        if (num_calls > 0) {
            Debug.Log("Update - enter");
            num_calls = num_calls - 1;
            AddToScore(100);
            Debug.Log("Update - exit");
        }
    }

    public void AddToScore(int scoreToBeAdded){
        Debug.Log("AddToScore - enter");
        if (scoreToBeAdded > 0){
            m_Score += scoreToBeAdded;
            CurrentScore.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(m_Score);
        }
        Debug.Log("AddToScore - current score is " + m_Score.ToString());
        Debug.Log("AddToScore - exit");

    }
}
