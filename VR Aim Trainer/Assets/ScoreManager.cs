using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    private int m_Score;

    private GameObject CurrentScore;

    void Start(){
        CurrentScore = GameObject.Find("ENVIRONMENT");
    }

    public void AddToScore(int scoreToBeAdded){
        if (scoreToBeAdded > 0){
            m_Score += scoreToBeAdded;
        }

        Debug.Log(CurrentScore.name);

        // CurrentScore = transform.Find("CurrentScore");
        // CurrentScore.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(m_Score);
    }
}
