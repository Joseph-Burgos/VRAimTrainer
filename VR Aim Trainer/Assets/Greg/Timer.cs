using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    bool active = true;
    float currentTime = 0f;
    float defaultInitialTime = 10f; // TODO take as parameter

    [SerializeField] GameObject timerDisplay;
    TextMeshProUGUI countdownText;
    [SerializeField] float initialTime;

    void Awake() {
        // retrieve the timerDisplay object and the countdownText
    }

    // Start is called before the first frame update
    void Start()
    {
        initialTime = defaultInitialTime;
        currentTime = initialTime; // TODO fix me this is bad
        // retrieve   
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            currentTime -= 1 * Time.deltaTime;
            timerText.text = currentTime.ToString("0");

            if (currentTime <= 0)
            {
                currentTime = 0;
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