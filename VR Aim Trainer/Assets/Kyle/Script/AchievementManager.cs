using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementManager : MonoBehaviour
{
    // Mainhub
    private int m_MainHubScoreAchieved;
    private int m_MainHubScore;
    [SerializeField]
    private int m_MainHubScoreGoal;
    private string m_MainHubName = "MainHub Remake";
    private string m_MainHubNameLoop = "MainHub Remake 1";

    // Tracking
    private int m_TrackingScoreAchieved;
    private int m_TrackingScore;
    private int m_TrackingScoreGoal = 40_000;

    // Misc.
    private int m_ShootFirstTargetAchieved;

    [SerializeField]
    private int m_TargetsHitGoal;
    private int m_TargetsHit = 0;
    private int m_TargetsHitAchieved;

    // used for making the object a singleton
    public static AchievementManager m_Instance;

    public GameObject m_GameSystem;

    // Start is called before the first frame update
    void Start()
    {
        // returns a -1 if the goal is not met, returns a 1 otherwise
        m_TargetsHit = PlayerPrefs.GetInt("TargetsHit", 0);
        m_TargetsHitAchieved = PlayerPrefs.GetInt("TargetsHitAchieved", -1 );
        m_MainHubScoreAchieved = PlayerPrefs.GetInt("MainHubScore", -1 );
        m_TrackingScoreAchieved = PlayerPrefs.GetInt("TrackingScore", -1 );
        m_ShootFirstTargetAchieved = PlayerPrefs.GetInt("ShootFirstTarget", -1 );
    }

    
    void Awake(){
        if (m_Instance == null){
            m_Instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update(){
        UpdateTargetsHitTMP();
        CheckMainHubScore();
        CheckTrackingScore();
    }

    // Increases the number of targets hit
    public void IncreaseTargetsHit(){
        m_TargetsHit += 1;
        PlayerPrefs.SetInt("TargetsHit", m_TargetsHit);
        PlayerPrefs.Save();

        GameObject.Find( "Shoot First Target TMP" ).GetComponent<TMPro.TextMeshProUGUI>().text = "Shoot First Target:\nCompleted";
        if (m_ShootFirstTargetAchieved != 1){
            m_ShootFirstTargetAchieved = 1;
            PlayerPrefs.SetInt("ShootFirstTarget", m_ShootFirstTargetAchieved);
            PlayerPrefs.Save();
        }
    }

    private void UpdateTargetsHitTMP(){
        Scene currentScene = SceneManager.GetActiveScene();

        // Checks if goal is reached
        if (m_TargetsHit > m_TargetsHitGoal)
        {
            m_TargetsHitAchieved = 1;
            PlayerPrefs.SetInt("TargetsHitAchieved", m_TargetsHitAchieved);
            PlayerPrefs.Save();
        }

        try{
            if (m_TargetsHit > 0){
                GameObject.Find( "Shoot First Target TMP" ).GetComponent<TMPro.TextMeshProUGUI>().text = "Shoot First Target:\nCompleted";
            }
        }
        catch (Exception e){
        }
        

        // checks if in the main hub
        if (currentScene.name.Equals(m_MainHubName) || currentScene.name.Equals(m_MainHubNameLoop))
        {
            if (m_TargetsHit > m_TargetsHitGoal){
                // Goal is completed
                string tempString = "Hit " + m_TargetsHitGoal + " Targets: " + m_TargetsHitGoal + " / " + m_TargetsHitGoal;
                GameObject.Find("Targets Hit TMP").GetComponent<TMPro.TextMeshProUGUI>().text = tempString;
            }
            else{
                // Goal is incomplete
                string tempString = "Hit " + m_TargetsHitGoal + " Targets: " + m_TargetsHit + " / " + m_TargetsHitGoal;
                GameObject.Find("Targets Hit TMP").GetComponent<TMPro.TextMeshProUGUI>().text = tempString;
            }   
        }
    }

    private void CheckMainHubScore(){
        Scene currentScene = SceneManager.GetActiveScene();
        // if not in the Main Hub
        if (!(currentScene.name.Equals(m_MainHubName) || currentScene.name.Equals(m_MainHubNameLoop)))
        {
            return;
        }

        // Retrieves the score from the main hub
        m_MainHubScore = m_GameSystem.GetComponent<ScoreManager>().GetScore();

        // if mainhub score has already been achieved or if mainhub score reached goal set mainhub score
        if ((m_MainHubScoreAchieved > 0) || (m_MainHubScore > m_MainHubScoreGoal)){
            GameObject.Find("MainHub Score TMP").GetComponent<TMPro.TextMeshProUGUI>().text = "Reach a score of " + m_MainHubScoreGoal + " in the main hub: Completed";
            m_MainHubScoreAchieved = 1;
            PlayerPrefs.SetInt("MainHubScore", m_MainHubScoreAchieved);
            PlayerPrefs.Save();
        }
    }

    // still needs to update score variable when in tracking scene
    public void CheckTrackingScore(){
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name.Equals("Tracking"))
        {
            // check and update score
            m_TrackingScore = GameObject.Find("GameSystem").GetComponent<ScoreManager>().GetScore();

            Debug.Log(m_TrackingScore);

            // check if the goal has been reached
            if (m_TrackingScore >= m_TrackingScoreGoal) {
                m_TrackingScoreAchieved = 1;
                PlayerPrefs.SetInt("TrackingScoreAchieved", m_TrackingScoreAchieved);
                PlayerPrefs.Save();
            }
        }
        else if (!(currentScene.name.Equals(m_MainHubName) || currentScene.name.Equals(m_MainHubNameLoop)))
        {
            Debug.Log(m_TrackingScoreAchieved);
            if (m_TrackingScoreAchieved == 1 || m_TrackingScore > m_TrackingScoreGoal){
                GameObject.Find( "Tracking Score TMP" ).GetComponent<TMPro.TextMeshProUGUI>().text = "Reach a Score of " + m_TrackingScoreGoal + "in Tracking Mode:\nCompleted";
            }
            else
            {
                GameObject.Find( "Tracking Score TMP" ).GetComponent<TMPro.TextMeshProUGUI>().text = "Reach a Score of " + m_TrackingScoreGoal + "in Tracking Mode:\nIncomplete";
            }
        }
    }
}
