using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGeneral : MonoBehaviour
{
    public GameObject m_OriginalPlayer;

    private static UserGeneral Instance;
    private GameObject m_player;

    private Vector3 spawnPoint;

    private SceneLoader SL = new SceneLoader();


    void Awake(){
        // set the original spawn point
        // spawnPoint = m_OriginalPlayer.transform.position;

        // delete the default player (Singleton)
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // Destroy();
            Debug.Log("Destroyed Instance");
            return;
        }

        // DontDestroyOnLoad(gameObject);

        // set the player location to that of the original / default
        // m_player = GameObject.Find("Player");
        // m_player.transform.position = spawnPoint;
        // if (SL.GetSceneName().Equals("MainHub 1")){
        //     m_player.transform.rotation = Quaternion.Euler(0, 90, 0);
        // }
    }
}
