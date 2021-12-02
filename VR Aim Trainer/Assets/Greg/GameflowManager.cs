using System;
using System.Collections;
using UnityEngine;



public class GameflowManager : MonoBehaviour {
    public enum StateType {
        NOTSTARTED, // Game hasn't yet started
        RUNNING, // Game is active, time remains on the clock, and we are not PAUSED
        PAUSED, // Player has activated the PAUSED mode, time is suspended, as are targets
        FINISHED // Game is over, time has expired
    };
    public StateType state;
    public string gamemode;
    [SerializeField] GameObject GameSystem;
    [SerializeField] GameObject TargetManager;
    [SerializeField] GameObject VisualFeedback;
    private VisualFeedback VisualFeedbackScript;
    private SaveManager saveManager;
    private ScoreManager scoreManager;
    private Timer timer;
    private TargetManager targetManager;
    // flags
    private bool menuActive;
 

    void Awake () {
        Debug.Log("GameflowManager: Awake()");
        state = StateType.NOTSTARTED;
        menuActive = false;
    }

    void Start () {
        Debug.Log("GameflowManager: Start() - grabbing timer");
        // Get the timer from the GameSystem object
        timer = GameSystem.GetComponent<Timer>();
        VisualFeedbackScript = VisualFeedback.GetComponent<VisualFeedback>();
        saveManager = GameSystem.GetComponent<SaveManager>();
        scoreManager = GameSystem.GetComponent<ScoreManager>();
        // Get the target manager from the GameManager object
        targetManager = TargetManager.GetComponent<TargetManager>();
        timer.StartTimer();
        Debug.Log("GameflowManager: Start() - successfully grabbed timer");
        state = StateType.RUNNING;

    }

    void Update () {
        // Debug.Log("GameflowManager: Update()");
        if (!timer.timeLeft() && !menuActive) {
            // Debug.Log("GameflowManager: Update(): Game Over!");
            menuActive = true;
            state = StateType.FINISHED;
            // signal target manager that game is finished
            targetManager.keepUpdating = false; 
            
            // save game data to server and disk
            // get time
            DateTime now = System.DateTime.Now;
            string dateTime = now.ToString("s");
            // get score
            int score = scoreManager.GetScore();
            // get accuracy
            float shots = (float)scoreManager.GetShots();
            float hits = (float)scoreManager.GetHits();
            float accuracy = 0;
            if (shots > 0) {
                accuracy = hits / shots;
            } 
            
            // create the playerscore object
            PlayerScore playerScore = new PlayerScore{
                dateTime = dateTime,
                gameMode = gamemode,
                score = score,
                gameTime = 54,
                accuracy = accuracy
            };
            // send the playerscore object to the SaveManager
            saveManager.addScore(playerScore);

            // expose post game display and menus to player
            VisualFeedback.SetActive(true);
            VisualFeedbackScript.initializeVisualFeedback();
        }

        // TESTER CODE - TEST FUNCTIONS ON KEYBOARD PRESS
        if (Input.GetKeyDown(KeyCode.P)) {
            Pause();

        }
    }

    void Pause () {
        Debug.Log("We are in the Pause() function. Game state is " + state);
        if (state == StateType.RUNNING) {
            state = StateType.PAUSED;
            timer.StopTimer();
        //     targetManager.pause(); 
            // TODO pause targets
        } else if (state == StateType.PAUSED) {
            state = StateType.RUNNING;
            timer.StartTimer();
            // targetManager.Resume();
            // TODO unpause targets
        }
    }

    public void saveGameData () {
        Debug.Log("We are in the saveGameData function.");
        // string userName = User.getUserName();
        // int score = ScoreManager.getScore();
        // string mode = "default"; // TODO handle modes
        // int time = timer.getInitialTime();
        // SaveManager.saveScore(score, userName, time, mode);
    }

}