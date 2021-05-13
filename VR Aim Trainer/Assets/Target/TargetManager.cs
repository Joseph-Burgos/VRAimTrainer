using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this Target spawner managert is used to spawn objects within given volume parameters.
//It also keeps track of all targets, hit or not.
//This list of targets and their info can be used to generate data/scores.
public class TargetManager : MonoBehaviour
{
    //max amount of targets that can be on screen
    public static int maxTargets = 3;
    //current number of targets on scene
    public static int currentTargets = 0;
    //list of targets to calculate score
    public static List<Target> targets = new List<Target>();
    //whether the game should be keeping track of the targets or not
    public static bool isRecording = false;

    //location and size of where target will be spawned
    [Tooltip("Location of center of cube to spawn targets")]
    public Transform center;
    [Tooltip("size of cube to spawn targets")]
    public Vector3 size;


    //object to spawn
    [Tooltip("Target Object to spawn")]
    public GameObject target;



    // Start is called before the first frame update
    void Start()
    {
        spawnTarget();
    }

    void Update()
    {
        //if the number of targets in level is less than 3, spawn more targetrs
        if (currentTargets < 3){
            spawnTarget();
            //Debug.Log(targets.Count);
        }

    }

    //when target is no longer needed, add to array in target  manager for score
    public static void addTarget(Target target)
    {
        //add target to list of targets hit, only if target manager wants to record
        if (isRecording)
        {
            targets.Add(target);
        }
        
        //update the max amount of targets on screen
        currentTargets = currentTargets - 1;
        //destroy object
        Destroy(target.gameObject);
        //Debug.Log(targets.Count);
        
    }

    public void spawnTarget()
    {
        //create new random position within the size of the square
        Vector3 newPos = center.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        //create a target at the new position
        Instantiate(target, newPos, Quaternion.identity);
        //update amount of targets on screen
        currentTargets = currentTargets + 1;
    }

    //visual volume of size of box where objects will be spawned.
    void OnDrawGizmosSelected()
    {
        //color and opacity of gizmo
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        //size and location of gizmo
        Gizmos.DrawCube(center.position, size);
    }


}