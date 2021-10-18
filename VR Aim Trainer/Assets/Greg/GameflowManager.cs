using UnityEngine;
// using System;

// public enum StateType {
//     NOTSTARTED, // Game hasn't yet started
//     RUNNING, // Game is active, time remains on the clock, and we are not PAUSED
//     PAUSED, // Player has activated the PAUSED mode, time is suspended, as are targets
//     FINISHED // Game is over, time has expired
// }

public class GameflowManager : MonoBehaviour {
    // private StateType state;
    // public GameObject otherGameObject;
    // private Timer timer;
    // private ScoreManager scoreManager;
    // private TargetManager targetManager;
    // private PlaytimeHistory playtimeHistory;

    void Awake () {
        Debug.Log("GameflowManager: Awake()");
        // state = NOTSTARTED;
        // timer = otherGameObject.GetComponent<Timer>();
        // targetManager = otherGameObject.GetComponent<TargetManager>();
        // playtimeHistory = otherGameObject.GetComponent<PlaytimeHistory>();
    }

    void Start () {
        // TODO add and start a countdown timer
        Debug.Log("GameflowManager: Start()");
        // state = RUNNING;
    }

    void Update () {
        Debug.Log("Update()");
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
        // if (state == RUNNING) {
        //     state = PAUSED;
        //     timer.Stop();
        //     targetManager.pause(); 
        //     // TODO pause targets
        // } else if (state == PAUSED) {
        //     state = RUNNING;
        //     timer.Resume();
        //     targetManager.Resume();
        //     // TODO unpause targets
        // }
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