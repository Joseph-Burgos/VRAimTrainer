using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//object spawns, waits 2 seconds, and invokes addTarget which adds itself to a list of the game manager and destroys itself
public class Target : MonoBehaviour
{
    //audio source to play when target is destroyed
    [Tooltip("audio source to play when target is destroyed")]
    public AudioClip hitSound;
    //particle explosion effect to spawn
    [Tooltip("effect on destroyed object")]
    public GameObject burst;
    //turn off timer when necessary
    protected bool timerActive = true;
    //is hit
    protected bool isHit = false;
    //max lifetime of target
    protected int maxLife = 5;
    //lifetime of target when being added for score
    protected float time = 0;

    // Update is called once per frame
    void Update()
    {
        //check if timer should be recording
        if (timerActive)
        {
            //update the time that the target has been alive   
            time += Time.deltaTime;
            //Debug.Log(time);
        }

    }



    //if hit by player weapon, add the target
    public void hit()
    {
        //stop timer from recording
        timerActive = false;
        Debug.Log("time: " + this.time);
        //add target to list
        TargetManager.addTarget(this);
        //play audio
        AudioSource.PlayClipAtPoint(hitSound, this.transform.position);
        //create a particle
        GameObject burstObject = Instantiate(burst, this.transform.position, Quaternion.identity);
        //destroy particle upon complete
        Destroy(burstObject, 1f);
    }



    ////when target is no longer needed, add to array in target  manager for score
    //protected void addTarget()
    //{
    //    //add target to list of targets hit
    //    //Debug.Log(this.time);
    //    TargetManager.targets.Add(this);
    //    //update the max amount of targets on screen
    //    TargetManager.currentTargets = TargetManager.currentTargets - 1;
    //    //destroy object
    //    Destroy(this.gameObject);
    //}
}
