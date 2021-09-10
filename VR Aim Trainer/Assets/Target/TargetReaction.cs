using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetReaction : Target
{
    MeshRenderer targetMeshRenderer;
    //color that we want
    [Tooltip("Color that we want to change to")]
    public Color newColor;
    float lerpTime;
    // Start is called before the first frame update

    float currentTime = 0f;
    void Start()
    {
        currentTime += Time.deltaTime;
        lerpTime = this.maxLife - 1.0f;
        targetMeshRenderer = GetComponent<MeshRenderer>();
        //execute a code snippet after a time finishes
        Invoke("expire", this.maxLife);
    }

    private void Update()
    {
        targetMeshRenderer.material.color = Color.Lerp(targetMeshRenderer.material.color, newColor, currentTime / lerpTime);
    }



    //when time runs out, add target with the max amount of time
    private void expire()
    {
        //stop timer from recording
        timerActive = false;
        //set time to max life if too high or low
        time = maxLife;
        Debug.Log("time: "+ this.time);
        TargetManager.addTarget(this);
    }


}
