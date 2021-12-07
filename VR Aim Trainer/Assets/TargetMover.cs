using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Target;
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private GameObject m_GameSystem;

    [Tooltip("Location of center of cube to spawn targets")]
    public Transform center;
    [Tooltip("size of cube to spawn targets")]
    public Vector3 size;

    private bool TargetMoving = false;

    private bool m_TimeEnabled = false;

    // Update is called once per frame
    void Update()
    {
        if (!TargetMoving && m_TimeEnabled){
            Vector3 NextPosition = center.position + 
                new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            StartCoroutine(MoveOverSpeed(m_Target, NextPosition, m_Speed));
        }
    }

    // Moves the target from one point to another
    // 1 speed is 1 unity per second
    public IEnumerator MoveOverSpeed (GameObject objectToMove, Vector3 end, float speed){
        TargetMoving = true;

        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame ();
        }
        TargetMoving = false;
    }

    public void SetDifficultyEasy(){
        m_Speed = 5;
    }

    public void SetDifficultyMedium(){
        m_Speed = 8;
    }

    public void SetDifficultyHard(){
        m_Speed = 10;
    }

    // Editor Visualization
    void OnDrawGizmosSelected()
    {
        //color and opacity of gizmo
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        //size and location of gizmo
        Gizmos.DrawCube(center.position, size);
    }

    public void SetTimeEnable(){
        Debug.Log("Time Enabled TargetMover");
        m_TimeEnabled = true;
    }

    public void SetTimeDisable(){
        Debug.Log("Time Disabled TargetMover");
        m_TimeEnabled = false;
    }
}
