using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrack : Target_Parent
{
    public float hitCount = 3.0f;
    public float totalCount = 3.0f;
    


    public override void hit(){
        hitCount+= Time.deltaTime;
        Debug.Log("Hit timer: " + hitCount);
    }

    void Update()
    {
        
        if(totalCount >= 20){
            Time.timeScale = 0;
            float timer = hitCount/totalCount;
            Debug.Log("Accuracy is ", timer);
        }
        totalCount+= Time.deltaTime;
        //Debug.Log("Total Timer: " + totalCount);
        

    }
}
