using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    bool active = false; // Indicates timer is running (game has been started and is not paused or finished)
    float currentTime = 0f; // Time that will be displayed in the timer

    public UnityEvent TimerStart, TimerStop;

    [SerializeField] GameObject timerDisplay; // object containing TMPro component 
    [SerializeField] float initialTime = 10f; // FIXME can this be a parameter somehow?

    void Awake() {
        // Debug.Log("Timer: Awake()");
    }

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Timer: Start()" + currentTime.ToString("0"));
        currentTime = initialTime; // set the length of game
        // retrieve the countdownText from the timerDisplay and update it
        timerDisplay.GetComponent<TMPro.TextMeshPro>().text = currentTime.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Timer - Update(): current time is " + currentTime.ToString("0"));
        if (active) {
            currentTime -= 1 * Time.deltaTime; // decrement timer
            timerDisplay.GetComponent<TMPro.TextMeshPro>().text = currentTime.ToString("0"); 

            if (currentTime <= 0)
            {
                currentTime = 0;
                active = false;  // Game is over
            }
        }
    }

    // This method allows other objects to poll 
    public bool timeLeft() {
        bool response = true;
        if (currentTime <= 0) { response = false; }
        return response;
    }

    public float getTimeRemaining() {
        return currentTime;
    }

    public float getInitialTime() {
        return initialTime;
    }

    private void ResetTimer(){
        currentTime = initialTime;
    }

    public void StartTimer () {
        ResetTimer();
        active = true;
        this.gameObject.GetComponent<ScoreManager>().SetScore((int)0);
        TimerStart.Invoke();
    }

    public void StopTimer () { 
        active = false; 
        TimerStop.Invoke();
    }
}