using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour

{
    private NavMeshAgent agent;

    public float radius;

    private void Start ()
    {
        agent = GetComponent<NavMeshAgent>();//find the NavMeshAgent Object in the scene to be referenced
    }

    private void Update ()
    {
        if (!agent.hasPath)//checks to see if the NavMeshObject has an area to travel to
        {
            agent.SetDestination (GetPoint.Instance.GetRandomPoint (transform, radius)); //gets a random position called from the GetPoint script calling the function GetRandomPoint
        }

    }

#if UNITY_EDITOR

    private void OnDrawGizmos ()//draws a visual representation on the given area of where the object can go
    {
        Gizmos.DrawWireSphere (transform.position, radius);
    }

#endif
}