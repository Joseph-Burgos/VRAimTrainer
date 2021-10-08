using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai : Target_Parent
{
    public float hitCount = 2.0f;
    public float totalCount = 2.0f;

    //public Transform startPos, endPos;
    public bool repeatable = false;
    public float speed = 1.0f;
    float startTime, totalDistance;
    private NavMeshAgent agent;

    public float radius;

    public override void hit(){
        hitCount+= Time.deltaTime;
        Debug.Log("Hit timer: " + hitCount);
    }
    private void Start ()
    {
        agent = GetComponent<NavMeshAgent> ();
    }

    private void Update ()
    {
        if (!agent.hasPath)
        {
            agent.SetDestination (GetPoint.Instance.GetRandomPoint (transform, radius));
        }
        if(!repeatable){
            float currentDuration = (Time.time - startTime) * speed;
            float journeyFraction = currentDuration / totalDistance;
        }
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

#if UNITY_EDITOR

    private void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere (transform.position, radius);
    }

#endif
}