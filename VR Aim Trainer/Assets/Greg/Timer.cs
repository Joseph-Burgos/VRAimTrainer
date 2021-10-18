using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    bool active = true; // Indicates timer is running (game has been started and is not paused or finished)
    float currentTime = 0f; // Time that will be displayed in the timer

    [SerializeField] GameObject timerDisplay; // object containing TMPro component 
    [SerializeField] float initialTime = 10f; // FIXME can this be a parameter somehow?

    void Awake() {
        Debug.Log("Timer: Awake()");
        // active = false; // FIXME delete, for debug
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Timer: Start()" + Convert.ToString(initialTime));
        currentTime = initialTime; // set the length of game
        // retrieve the countdownText from the timerDisplay and update it
        timerDisplay.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(currentTime);
        // currentTime.ToString("0");
        Debug.Log("Timer - Start(): current time is " + Convert.ToString(currentTime));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Timer - Update(): current time is " + Convert.ToString(currentTime));
        if (active) {
            currentTime -= 1 * Time.deltaTime; // decrement timer
            timerDisplay.GetComponent<TMPro.TextMeshPro>().text = Convert.ToString(currentTime); 

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

    public void Stop () { active = false; }

    public void Resume () { active = true; }


}