using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatedTracking : MonoBehaviour
{
    public GameObject target = GameObject.Find("TrackingTarget");
    public Transform starting = target.position;
    public Transform startPos, endPos;
    public float speed = 1.0f;
    public bool repeatable = false;

    private float startTime;
    private float journeyLength;
    public float duration = 3.0f;
    public Vector3[] allPos = new Vector3[4];

    // Start is called before the first frame update
    IEnumerator Start()
    {
        allPos[0] = new Vector3(Random.Range(-3.0f,12.0f), 6, Random.Range(-4.0f,10.0f));
        allPos[1] = new Vector3(Random.Range(-3.0f,12.0f), 6, Random.Range(-4.0f,10.0f));
        allPos[2] = new Vector3(Random.Range(-3.0f,12.0f), 6, Random.Range(-4.0f,10.0f));
        allPos[3] = new Vector3(Random.Range(-3.0f,12.0f), 6, Random.Range(-4.0f,10.0f));
        startTime = Time.time;
        startPos = starting;
        endPos = allPos[0];
        journeyLength = Vector3.Distance(startPos.position,endPos.position);
        while(repeatable){
            if(startPos == starting){
                endPos = allPos[0];
                yield return RepeatLerp(startPos.position, endPos.position,duration);
                yield return RepeatLerp(endPos.position, startPos.position,duration);
            }
            else{

                startPos = 
                endPos = 
                yield return RepeatLerp(startPos.position, endPos.position,duration);
                yield return RepeatLerp(endPos.position, startPos.position,duration);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!repeatable){
            float currentDuration = (startTime.time - startTime) * speed;
            float journeyFraction = currentDuration / journeyLength;
            this.transform.position = Vector3.Lerp(startPos.position, endPos.position, journeyFraction);
        }
    }

    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time){
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while( i < 1.0f){
            i += Time.deltaTime * rate;
            this.transform.position = Vector3.Lerp(a,b,i);
            yield return null;
        }
    }
}
