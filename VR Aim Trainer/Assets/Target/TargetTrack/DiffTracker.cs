using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffTracker : Target_Parent
{

    private float desiredDuration =3f;
    private float elapsedTime;
    private Vector3 startPosition;



    void Start(){

        startPosition = transform.position;
    }
    

    void Update(){
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime/ desiredDuration;
        NextLerp(percentageComplete);
    }

    public override void hit(){
        
    }

    void NextLerp(float x){
        transform.position = Vector3.Lerp(startPosition, new Vector3(Random.Range(-3.0f,12.0f), 6, Random.Range(-4.0f,10.0f)), x);
        startPosition = new Vector3(Random.Range(-3.0f,12.0f), 6, Random.Range(-4.0f,10.0f));
    }
}
