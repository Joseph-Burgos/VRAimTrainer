using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

// This class defines the behaviour of a countdown timer that limits the amount of time a 
// player may be active in any given stage. Time allowed can be set from the Unity Editor.
public class Timer : MonoBehaviour
{
    bool active = false; // Indicates timer is running (game has been started and is not paused or finished)
    float currentTime = 0f; // Time that will be displayed in the timer

    public UnityEvent TimerStart, TimerStop;

    [SerializeField] GameObject timerDisplay; // object containing TMPro component 
    [SerializeField] float initialTime = 10f; // default initial time

    void Awake() {
        // Debug.Log("Timer: Awake()");
    }

    void Start()
    {
        // Debug.Log("Timer: Start()" + currentTime.ToString("0"));
        currentTime = initialTime; // set the length of game

        // retrieve the countdownText from the timerDisplay and update it
        timerDisplay.GetComponent<TMPro.TextMeshPro>().text = currentTime.ToString("0");
    }

    void Update()
    {
        // Debug.Log("Timer - Update(): current time is " + currentTime.ToString("0"));
        if (active) {
            currentTime -= 1 * Time.deltaTime; // decrement timer
            timerDisplay.GetComponent<TMPro.TextMeshPro>().text = currentTime.ToString("0"); 

            if (currentTime <= 0)
            {
                currentTime = 0;
                StopTimer();  // Game is over
            }
        }
    }

    // This method allows other objects to poll remaining time
    public bool timeLeft() {
        bool response = true;
        if (currentTime <= 0) { response = false; }
        return response;
    }

    // Getter for current time remaining in stage.
    public float getTimeRemaining() {
        return currentTime;
    }

    // Getter for the initial time defined from Editor.
    public float getInitialTime() {
        return initialTime;
    }

    // resets the timer
    private void ResetTimer(){
        currentTime = initialTime;
    }

    // starts the countdown and resets score to 0
    public void StartTimer () {
        ResetTimer();
        active = true;
        // this.gameObject.GetComponent<ScoreManager>().SetScore((int)0);
        GameObject.Find("GameSystem").GetComponent<ScoreManager>().SetScore((int)0);

        TimerStart.Invoke();
    }

    // stops countdown
    public void StopTimer () { 
        active = false; 
        TimerStop.Invoke();
        FindObjectOfType<AudioManager>().Play("Buzzer");
    }
}