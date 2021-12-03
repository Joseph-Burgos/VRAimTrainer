using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetPoint : MonoBehaviour
{

    public static GetPoint Instance;

    public float Range;

    private void Awake ()
    {
        Instance = this;//what ever object is connected to this script
    }

    bool RandomPoint (Vector3 center, float range, out Vector3 result)
    {

        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;// gets random point in given area
            NavMeshHit hit;
            if (NavMesh.SamplePosition (randomPoint, out hit, 1.0f, NavMesh.AllAreas))//checks the NavMeshObject if it reaches the random point
            {
                result = hit.position;
                return true;// moves on to get a new position
            }
        }

        result = Vector3.zero;// resets to default 0,0,0 if can not reach certain point

        return false;
    }

    public Vector3 GetRandomPoint (Transform point = null, float radius = 0)//gets called into the Ai.cs script to be given random positions
    {
        Vector3 _point;// empty Vector3

        if (RandomPoint (point == null?transform.position : point.position, radius == 0 ? Range : radius, out _point))//checks to see if the RandomPoint function has a real position
        {
            Debug.DrawRay (_point, Vector3.up, Color.black, 1); //draws out random positions of where the NavMeshObject could go to

            return _point;
        }

        return point == null? Vector3.zero : point.position; // returns a default Vector3 position 0,0,0
    }

#if UNITY_EDITOR
    private void OnDrawGizmos ()//draws a visual representation on the given area of where the object can go
    {
        Gizmos.DrawWireSphere (transform.position, Range);
    }

#endif

}