using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaytimeManager : MonoBehaviour
{
    public static PlaytimeManager m_Instance;

    private float m_SecondsPlayed;

    void Awake()
    {
        if (m_Instance == null){
            m_Instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_SecondsPlayed += Time.time;
    }

    void OnApplicationQuit()
    {
        UpdatePlayTime();
    }

    void OnDestroy(){
        UpdatePlayTime();
    }

    private void UpdatePlayTime(){
        float time = PlayerPrefs.GetFloat("Playtime", 0) + m_SecondsPlayed;
        PlayerPrefs.SetFloat("Playtime", time);
        Debug.Log(time + "");

        PlayerPrefs.Save();

        m_SecondsPlayed = 0;
    }
}
