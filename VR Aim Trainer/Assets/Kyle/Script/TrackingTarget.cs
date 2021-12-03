using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingTarget : Target_Parent
{
    private float m_TotalTimeHit = 0;
    public bool m_TimeEnabled = true;
    public float m_Time = 0;

    public GameObject m_GameSystem;

    void Update(){

    }

    public override void hit()
    {
        m_TotalTimeHit += Time.deltaTime;
        Debug.Log(m_TotalTimeHit);
        m_GameSystem.GetComponent<ScoreManager>().SetScore((int)Math.Round(100 * m_TotalTimeHit, 0));
    }

    public float GetTotalTime()
    {
        return m_TotalTimeHit;
    }

}