using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrack : Target_Parent
{
    //color info
    [SerializeField]
    private Material Current_Mat;
    private Color Highlight_Color = Color.green;
    private Color default_Color = Color.cyan;
    private bool isHighlighted;
    private float temp_time;
    //vfx info
    public ParticleSystem Charge_Effect = null;
    public Transform startPos, endPos;
    //private bool changeColor;

    //tracking time
    public float hitCount = 2.0f;
    public float totalCount = 2.0f;

    public bool repeatable = false;
    public float speed = 1.0f;
    float startTime, totalDistance;
    IEnumerator Start(){
        startTime = Time.time;
        Current_Mat.color = default_Color;
        temp_time = 0;
        isHighlighted = false;
        totalDistance = Vector3.Distance(startPos.position, endPos.position);
        while(repeatable){
            yield return RepeatLerp(startPos.position, endPos.position, 3.0f);
            yield return RepeatLerp(endPos.position, startPos.position, 3.0f);
            
        }

    } 

    // void Start()
    // {
    //     Current_Mat.color = default_Color;
    //     temp_time = 0;
    //     isHighlighted = false;
    // }
    void Update()
    {
        if (!repeatable)
        {
        float currentDuration = (Time.time - startTime) * speed;
        float journeyFraction = currentDuration / totalDistance;
        this.transform.position = Vector3.Lerp(startPos.position, endPos.position, journeyFraction);

        }
        totalCount+= Time.deltaTime;

        if(totalCount >= 200)
        {
            StopGame();
            totalCount = 0;
        }

        //COLOR CHANGE AND VFX---
        //if we were highlighted, gotta check if we stop becoming tracekd
        if (isHighlighted)
        {
            //if the difference in time shall differ at any point while "highlighted", then it is no longer highlighted
            if ((totalCount - hitCount) > temp_time)
            {
                //update to no longer being highlighted, change color
                isHighlighted = false;
                switchColor(default_Color);
            }
        }
        else
        {
            //constantly calculates the difference in time before being highlighted
            temp_time = totalCount - hitCount;
        }


        //play fancy vfx only if permitted by game manager-------------------
        if (GameManager.Instance.useVFX)
        {
            //play vfx if not playing
            if (isHighlighted)
            {
                if (!Charge_Effect.isPlaying)
                {
                    Charge_Effect.Play();
                }
            }
            //stop vfx
            else
            {
                if (Charge_Effect.isPlaying)
                {
                    Charge_Effect.Stop();
                }
            }
        }
    }

    public override void hit()
    {
        hitCount += Time.deltaTime;
        //Debug.Log("Hit timer: " + hitCount);

        //change color on hit
        if (!isHighlighted)
        {
            switchColor(Highlight_Color);
            isHighlighted = true;
        }

    }


    public void StopGame(){
        Time.timeScale = 0;
        float avg = hitCount / totalCount;
        Debug.Log("Average: " + (avg * 100) + "%");
        
    }


    public void switchColor(Color c)
    {
        Current_Mat.color = c;
    }

    public IEnumerator RepeatLerp (Vector3 a, Vector3 b, float time){
        float i = 0.0f;
        float rate = (1.0f/time) * speed;
        while(i<1.0f){
            i+= Time.deltaTime * rate;
            this.transform.position = Vector3.Lerp(a,b,i);
            yield return null;
        }
    }
    
}
