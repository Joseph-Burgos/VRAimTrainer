using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrack : Target_Parent
{
    public float hitCount = 2.0f;
    public float totalCount = 2.0f;


    public override void hit(){
        hitCount+= Time.deltaTime;
        Debug.Log("Hit timer: " + hitCount);
    }

    void Update()
    {
        
        totalCount+= Time.deltaTime;

        if(totalCount >= 20){
            StopGame();
            totalCount = 0;
        }
    }
    public void StopGame(){
        Time.timeScale = 0;
        float avg = hitCount / totalCount;
        Debug.Log("Average: " + (avg * 100) + "%");
        
    }
    
}
