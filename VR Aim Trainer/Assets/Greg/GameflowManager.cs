using UnityEngine;
using System.Collections;



public class GameflowManager : MonoBehaviour {
    public enum StateType {
        NOTSTARTED, // Game hasn't yet started
        RUNNING, // Game is active, time remains on the clock, and we are not PAUSED
        PAUSED, // Player has activated the PAUSED mode, time is suspended, as are targets
        FINISHED // Game is over, time has expired
    };
    public StateType state;
    [SerializeField] GameObject GameSystem;
    [SerializeField] GameObject TargetManager;
    private Timer timer;
    private TargetManager targetManager;
    // private ScoreManager scoreManager;
    
    // private PlaytimeHistory playtimeHistory;

    void Awake () {
        Debug.Log("GameflowManager: Awake()");
        state = StateType.NOTSTARTED;
        
        // playtimeHistory = otherGameObject.GetComponent<PlaytimeHistory>();
    }

    void Start () {
        // TODO add and start a countdown timer
        Debug.Log("GameflowManager: Start() - grabbing timer");
        // Get the timer from the GameSystem object
        timer = GameSystem.GetComponent<Timer>();
        // Get the target manager from the GameManager object
        targetManager = TargetManager.GetComponent<TargetManager>();
        timer.StartTimer();
        Debug.Log("GameflowManager: Start() - successfully grabbed timer");
        state = StateType.RUNNING;
    }

    void Update () {
        // Debug.Log("GameflowManager: Update()");
        if (!timer.timeLeft()) {
            Debug.Log("GameflowManager: Update(): Game Over!");
            state = StateType.FINISHED;
            
            // signal target manager that game is finished
            targetManager.keepUpdating = false;
            // PlaytimeHistory.calculatePlaytimeHistory();
            // var scoreHistory = PlaytimeHistory.generateScoreData();
            // var accHistory = PlaytimeHistory.generateAccuracyData();
            // saveGameData(); // TODO save game data to server and disk
            // TODO expose post game display and menus to player
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