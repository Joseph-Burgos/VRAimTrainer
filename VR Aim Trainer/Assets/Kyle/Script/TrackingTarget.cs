using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingTarget : Target_Parent
{
    private float m_TotalTimeHit = 0;
    // used for when the timer is active
    public bool m_TimeEnabled = true;

    [SerializeField] private GameObject m_GameSystem;

    // Updates the score when a target is being tracked
    public override void hit()
    {
        if (m_TimeEnabled){
            m_TotalTimeHit += Time.deltaTime;
            // Debug.Log(m_TotalTimeHit);
            m_GameSystem.GetComponent<ScoreManager>().SetScore((int)Math.Round(1000 * m_TotalTimeHit, 0));
        }
    }

    // returns the total time tracked
    public float GetTotalTime()
    {
        return m_TotalTimeHit;
    }

    public void SetTimeEnable(){
        m_TimeEnabled = true;
        m_TotalTimeHit = 0;
        // resets the score on start of a session
        m_GameSystem.GetComponent<ScoreManager>().SetScore( (int)0 );
    }

    public void SetTimeDisable(){
        m_TimeEnabled = false;
    }

}