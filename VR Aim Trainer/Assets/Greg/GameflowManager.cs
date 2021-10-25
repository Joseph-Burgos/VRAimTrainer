using UnityEngine;
using System.Collections;



public class GameflowManager : MonoBehaviour {
    public enum StateType {
        NOTSTARTED, 
        RUNNING, 
        PAUSED, 
        FINISHED
    };
    // public enum StateType {
    //     NOTSTARTED, // Game hasn't yet started
    //     RUNNING, // Game is active, time remains on the clock, and we are not PAUSED
    //     PAUSED, // Player has activated the PAUSED mode, time is suspended, as are targets
    //     FINISHED // Game is over, time has expired
    // }
    public StateType state;
    public GameObject otherGameObject;
    [SerializeField] GameObject GameSystem;
    private Timer timer;
    // private ScoreManager scoreManager;
    // private TargetManager targetManager;
    // private PlaytimeHistory playtimeHistory;

    void Awake () {
        Debug.Log("GameflowManager: Awake()");
        state = StateType.NOTSTARTED;
        
        // targetManager = otherGameObject.GetComponent<TargetManager>();
        // playtimeHistory = otherGameObject.GetComponent<PlaytimeHistory>();
    }

    void Start () {
        // TODO add and start a countdown timer
        Debug.Log("GameflowManager: Start() - grabbing timer");
        // Get the timer from the GameSystem object
        timer = GameSystem.GetComponent<Timer>();
        timer.StartTimer();
        Debug.Log("GameflowManager: Start() - successfully grabbed timer");
        state = StateType.RUNNING;
    }

    void Update () {
        // Debug.Log("GameflowManager: Update()");
        // if (!timer.timeLeft()) {
            // Debug.log("GameflowManager: Update(): Game Over!");
            // state = FINISHED;
            // saveGameData(); // save game data to server and disk
            // TODO signal target manager that game is finished
            // PlaytimeHistory.calculatePlaytimeHistory();
            // var scoreHistory = PlaytimeHistory.generateScoreData();
            // var accHistory = PlaytimeHistory.generateAccuracyData();
            // TODO expose post game display and menus to player
        // }
    }

    void Pause () {
        Debug.Log("We are in the Pause() function.");
        if (state == StateType.RUNNING) {
            state = StateType.PAUSED;
            // timer.Stop();
        //     targetManager.pause(); 
            // TODO pause targets
        } else if (state == StateType.PAUSED) {
            state = StateType.RUNNING;
            // timer.Resume();
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