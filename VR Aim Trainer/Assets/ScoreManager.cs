using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    private int m_Score;
    private int num_calls;

    private Transform scoreContainer; // parent of current score display
    private Transform scoreTMP; // object that has the text for the current score
    private TextMeshProUGUI z;

    private GameObject CurrentScore;

    void Start(){
        Debug.Log("Start - enter");
        //CurrentScore = GameObject.Find("CurrentScore");
        num_calls = 1;
        m_Score = 0;
        //Debug.Log("Start - Current score object " + CurrentScore.ToString());

        scoreContainer = transform.Find("Score");
        scoreContainer.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(m_Score);

        Debug.Log("Start - found current score container " + scoreContainer.ToString());
        Debug.Log("Start - exit");
    }

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
            //CurrentScore = transform.Find("Score");
            scoreContainer.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(m_Score);
        }
        Debug.Log("AddToScore - current score is " + m_Score.ToString());
        Debug.Log("AddToScore - exit");

    }
}
