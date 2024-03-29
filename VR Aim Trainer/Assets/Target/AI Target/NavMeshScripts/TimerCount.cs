using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCount : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 20;
    public bool takingAway = false;

    void Start(){
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
    }
    void Update(){
        if(takingAway == false && secondsLeft > 0){
            StartCoroutine(TimerTake());
        }
    }

    IEnumerator TimerTake(){
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        takingAway = false;
    }

}
