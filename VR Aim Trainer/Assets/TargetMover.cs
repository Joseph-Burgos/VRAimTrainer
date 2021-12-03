using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Target;
    [SerializeField]
    private float m_Speed;

    public GameObject m_Gun;

    [Tooltip("Location of center of cube to spawn targets")]
    public Transform center;
    [Tooltip("size of cube to spawn targets")]
    public Vector3 size;

    private bool TargetMoving = false;

    // Update is called once per frame
    void Update()
    {
        if (!TargetMoving){
            Vector3 NextPosition = center.position + 
                new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            StartCoroutine(MoveOverSpeed(m_Target, NextPosition, m_Speed));
        }
    }

    public void hit()
    {
        // todo fill this function
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

    public IEnumerator MoveOverSeconds (GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
    }

    // Editor Visualization
    void OnDrawGizmosSelected()
    {
        //color and opacity of gizmo
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        //size and location of gizmo
        Gizmos.DrawCube(center.position, size);
    }
}
