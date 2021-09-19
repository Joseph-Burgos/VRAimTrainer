using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrack : Target_Parent
{
    public float hitCount = 3.0f;
    public float totalCount = 3.0f;

    public Transform startPos, endPos;
    public bool repeatable = false;
    public float speed = 1.0f;
    float startTime, totalDistance;
    IEnumerator Start(){
        startTime = Time.time;
        totalDistance = Vector3.Distance(startPos.position, endPos.position);
        while(repeatable){
            yield return RepeatLerp(startPos.position, endPos.position, 3.0f);
            yield return RepeatLerp(endPos.position, startPos.position, 3.0f);
        }
    }


    public override void hit(){
        hitCount+= Time.deltaTime;
        Debug.Log("Hit timer: " + hitCount);
    }

    void Update()
    {
<<<<<<< HEAD
        if(!repeatable){
            float currentDuration = (Time.time - startTime) * speed;
            float journeyFraction = currentDuration / totalDistance;
            this.transform.position = Vector3.Lerp(startPos.position, endPos.position, journeyFraction);
=======
        
        if(totalCount >= 20){
            Time.timeScale = 0;
            float timer = hitCount/totalCount;
            Debug.Log("Accuracy is " + timer);
>>>>>>> c0880a9be230bf852c21f7f2fcd504d2aa9b71c1
        }
        totalCount+= Time.deltaTime;

        if(totalCount >= 20){
            StopGame();
            totalCount = 0;
        }
    }
    public void StopGame(){
        float avg = hitCount / totalCount;
        Debug.Log("Average: " + (avg * 100));
        Time.timeScale = 0;
    }
    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time){
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f){
            i += Time.deltaTime * rate;
            this.transform.position = Vector3.Lerp(a ,b , i);
            yield return null;
        }
    }
    
}
