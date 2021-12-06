using System;
using System.Collections;
using UnityEngine;


// This class handles auxiliary tasks required to play a time-limited stage.
public class GameflowManager : MonoBehaviour {
    // Defines current state of the game
    public enum StateType {
        NOTSTARTED, // Game hasn't yet started
        RUNNING, // Game is active, time remains on the clock, and we are not PAUSED
        PAUSED, // Player has activated the PAUSED mode, time is suspended, as are targets
        FINISHED // Game is over, time has expired
    };
    public StateType state;
    public string gamemode; // store reference to current gamemode (set in Unity Editor)
    // GameObjects (set in Unity to access components)
    [SerializeField] GameObject GameSystem;
    [SerializeField] GameObject VisualFeedback;
    [SerializeField] GameObject TrackingTargetObject;
    // GameObject components we will actually interact with
    private VisualFeedback VisualFeedbackScript;
    private SaveManager saveManager;
    private ScoreManager scoreManager;
    private Timer timer;
    private TargetTrack targetTrack;
    // flags
    private bool menuActive; // is the VisualFeedback menu active?
 

    void Awake () {
        // Debug.Log("GameflowManager: Awake()");
        // Initialize game state
        state = StateType.NOTSTARTED;
        menuActive = false;
        if (gamemode == "tracking") {
            targetTrack = TrackingTargetObject.GetComponent<TargetTrack>();
        } else {
            targetTrack = null;
        }
    }

    void Start () {
        // Debug.Log("GameflowManager: Start() - grabbing timer");
        // Get the class/behavioural components from their parent GameSystem objects
        timer = GameSystem.GetComponent<Timer>();
        VisualFeedbackScript = VisualFeedback.GetComponent<VisualFeedback>();
        saveManager = GameSystem.GetComponent<SaveManager>();
        scoreManager = GameSystem.GetComponent<ScoreManager>();
        // start timer and set game state to RUNNING
        timer.StartTimer();
        state = StateType.RUNNING;
    }

    void Update () {
        // // Debug.Log("GameflowManager: Update()");
        if (!timer.timeLeft() && !menuActive) {
            // // Debug.Log("GameflowManager: Update(): Game Over!");
            menuActive = true;
            state = StateType.FINISHED;
            
            // save game data to server and disk
            // get time
            DateTime now = System.DateTime.Now;
            string dateTime = now.ToString("s");
            // get score
            int score = scoreManager.GetScore();
            // get accuracy
            float accuracy = getAccuracy();
            
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

    // This function can be called to pause a RUNNING game
    void Pause () {
        // Debug.Log("We are in the Pause() function. Game state is " + state);
        if (state == StateType.RUNNING) {
            state = StateType.PAUSED;
            timer.StopTimer();
        } else if (state == StateType.PAUSED) {
            state = StateType.RUNNING;
            timer.StartTimer();
        }
    }

    // This function handles saving data from a FINISHED game
    public void saveGameData () {
        // Debug.Log("We are in the saveGameData function.");
        // string userName = User.getUserName();
        // int score = ScoreManager.getScore();
        // string mode = "default"; // TODO handle modes
        // int time = timer.getInitialTime();
        // SaveManager.saveScore(score, userName, time, mode);
    }

    public float getAccuracy() {
        float accuracy = 0;
        if (gamemode == "reaction") {
            float shots = (float)scoreManager.GetShots();
            float hits = (float)scoreManager.GetHits();
            if (shots > 0) {
                accuracy = hits / shots;
            } 
        } else if (gamemode == "tracking" && targetTrack != null) {
            // total time hit over initial time
            float hits = (float) targetTrack.hitCount;
            float total = (float) targetTrack.totalCount;
            if (total > 0) {
                accuracy = hits / total;
            }
        }
        return accuracy;
    }

}