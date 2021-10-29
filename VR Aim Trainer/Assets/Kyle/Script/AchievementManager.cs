using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementManager : MonoBehaviour
{
    private int m_TargetsHit;

    private int m_MainHubScoreAchieved;
    public int m_MainHubScoreGoal;

    private int m_TrackingScore;
    private int m_ShootFirstTarget;

    public static AchievementManager m_Instance;
    
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

    // Start is called before the first frame update
    void Start()
    {
        m_TargetsHit = PlayerPrefs.GetInt("TargetsHit", 0);
        m_MainHubScoreAchieved = PlayerPrefs.GetInt("MainHubScore", -1 );
        m_MainHubScoreAchieved = PlayerPrefs.GetInt("TrackingScore", -1 );
        m_ShootFirstTarget = PlayerPrefs.GetInt("ShootFirstTarget", -1 );
    }

    public void IncreaseTargetsHit(){
        m_TargetsHit += 1;
        PlayerPrefs.SetInt("TargetsHit", m_TargetsHit);
        PlayerPrefs.Save();

        if (m_ShootFirstTarget == -1){
            GameObject.Find( "Shoot First Target TMP" ).GetComponent<TMPro.TextMeshProUGUI>().text = "Shoot First Target:\nCompleted";
            m_ShootFirstTarget = 1;
            PlayerPrefs.SetInt("ShootFirstTarget", m_ShootFirstTarget);
        }

        int targetsHitGoal = 10000;
        if (m_TargetsHit > targetsHitGoal){
            string tempString = "Hit " + targetsHitGoal + " targets: " + targetsHitGoal + " / " + targetsHitGoal;
            GameObject.Find("Hit X targets").GetComponent<TMPro.TextMeshProUGUI>().text = tempString;
        }
        else{
            string tempString = "Hit " + targetsHitGoal + " targets: " + m_TargetsHit + " / " + targetsHitGoal;
            GameObject.Find("Hit X targets").GetComponent<TMPro.TextMeshProUGUI>().text = tempString;
        }
    }

    public void CheckMainHubScore(){
        Scene currentScene = SceneManager.GetActiveScene();
        if (!currentScene.name.Equals("MainHub 1")){
            return;
        }
        
        if (m_MainHubScoreAchieved > 0){
            GameObject.Find("Hit X targets").GetComponent<TMPro.TextMeshProUGUI>().text = " ";
        }

        // if mainhub score reached goal set mainhub score
    }
}
