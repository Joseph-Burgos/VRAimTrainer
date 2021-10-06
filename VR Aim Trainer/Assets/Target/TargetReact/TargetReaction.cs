using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetReaction : Target
{

    // Start is called before the first frame update
    void Start()
    {
        //execute a code snippet after a time passes
        Invoke("expire", maxLife);
    }



    //when time runs out, add target with the max amount of time
    private void expire()
    {
        //stop timer from recording
        timerActive = false;
        //set time to max life if too high or low
        time = maxLife;
        //when done, add target to list
        TargetManager.addTarget(this);
    }


}
