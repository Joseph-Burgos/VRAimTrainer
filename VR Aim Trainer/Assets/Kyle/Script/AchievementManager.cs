using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementManager : MonoBehaviour
{
    // Mainhub
    private int m_MainHubScoreAchieved;
    private int m_MainHubScore;
    [SerializeField]
    private int m_MainHubScoreGoal;

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

    // Start is called before the first frame update
    void Start()
    {
        // returns a -1 if the goal is not met, returns a 1 otherwise
        m_TargetsHit = PlayerPrefs.GetInt("TargetsHit", 0);
        m_TargetsHitAchieved = PlayerPrefs.GetInt("ShootFirstTarget", -1 );
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

        // initialize the text for all TMPs on the achievement canvas
        if (SceneManager.GetActiveScene().name.Equals("MainHub 1")){
            if (m_ShootFirstTargetAchieved == 1){
            GameObject.Find( "Shoot First Target TMP" ).GetComponent<TMPro.TextMeshProUGUI>().text = "Shoot First Target:\nCompleted";
            }
            else{
                GameObject.Find( "Shoot First Target TMP" ).GetComponent<TMPro.TextMeshProUGUI>().text = "Shoot First Target:\nIncomplete";
            }

            UpdateTargetsHitTMP();
        }

        CheckMainHubScore();
        CheckTrackingScore();
    }

    public void IncreaseTargetsHit(){
        m_TargetsHit += 1;
        PlayerPrefs.SetInt("TargetsHit", m_TargetsHit);
        PlayerPrefs.Save();

        if (m_ShootFirstTargetAchieved == -1){
            GameObject.Find( "Shoot First Target TMP" ).GetComponent<TMPro.TextMeshProUGUI>().text = "Shoot First Target:\nCompleted";
            m_ShootFirstTargetAchieved = 1;
            PlayerPrefs.SetInt("ShootFirstTarget", m_ShootFirstTargetAchieved);
        }

        UpdateTargetsHitTMP();
        CheckMainHubScore();
        CheckTrackingScore();
    }

    private void UpdateTargetsHitTMP(){
        if (m_TargetsHit > m_TargetsHitGoal){
            // Goal is completed
            string tempString = "Hit " + m_TargetsHitGoal + " Targets: " + m_TargetsHitGoal + " / " + m_TargetsHitGoal;
            GameObject.Find("Targets Hit TMP").GetComponent<TMPro.TextMeshProUGUI>().text = tempString;

            m_TargetsHitAchieved = 1;
            PlayerPrefs.SetInt("TargetsHit", m_TargetsHitAchieved);
            PlayerPrefs.Save();
        }
        else{
            // Goal is incomplete
            string tempString = "Hit " + m_TargetsHitGoal + " Targets: " + m_TargetsHit + " / " + m_TargetsHitGoal;
            GameObject.Find("Targets Hit TMP").GetComponent<TMPro.TextMeshProUGUI>().text = tempString;
        }
    }

    private void CheckMainHubScore(){
        Scene currentScene = SceneManager.GetActiveScene();
        // if not in the Main Hub
        if (!currentScene.name.Equals("MainHub 1")){
            return;
        }

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
            ScoreManager sm = new ScoreManager();
            m_TrackingScore = sm.GetScore();

            // check if the goal has been reached
            if (m_TrackingScore >= m_TrackingScoreGoal) {
                PlayerPrefs.SetInt("TrackingScore", 1 );
                PlayerPrefs.Save();
            }
        }
        else if (!currentScene.name.Equals("MainHub 1"))
        {
            return;
        }

        if (m_TrackingScoreAchieved == 1){
            GameObject.Find( "Tracking Score TMP" ).GetComponent<TMPro.TextMeshProUGUI>().text = "Reach a Score of " + m_TrackingScoreGoal + "in Tracking Mode:\nCompleted";
        }
        else{
            GameObject.Find( "Tracking Score TMP" ).GetComponent<TMPro.TextMeshProUGUI>().text = "Reach a Score of " + m_TrackingScoreGoal + "in Tracking Mode:\nIncomplete";
        }
    }
}
