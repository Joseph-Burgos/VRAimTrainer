using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;
    public VRInputModule m_InputModule;
    private LineRenderer m_LineRenderer = null;

    private bool m_RayCasted = true;
    private Valve.VR.InteractionSystem.Hand m_Hand = null;

    private void Awake() {
        m_LineRenderer = GetComponent<LineRenderer>();

        GameObject R_hand = this.transform.parent.gameObject;
        m_Hand = R_hand.GetComponent<Valve.VR.InteractionSystem.Hand>();
    }

    private void toggleLine(){
        if (!m_RayCasted){
            m_LineRenderer = GetComponent<LineRenderer>();
        }
        else{
            Vector3 hideLine = new Vector3(0f, 0f, 0f);
            m_Dot.transform.position = hideLine;
            m_LineRenderer.SetPosition(0, hideLine);
            m_LineRenderer.SetPosition(1, hideLine);
            m_LineRenderer = null;
        }
        m_RayCasted = !m_RayCasted;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_RayCasted){
            updateLine();

            if(m_Hand.ObjectInHand()){
                toggleLine();
            }
        }
        else{
            if(!m_Hand.ObjectInHand()){
                toggleLine();
            }
        }
    }

    private void updateLine(){
        // Use default or distance
        float targetLength = m_DefaultLength;

        // Raycast
        RaycastHit hit = CreateRaycast(targetLength);

        // Default End
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        // Or based on hit
        if (hit.collider != null){
            endPosition = hit.point;
        }
 
        // Set Position of the dot
        m_Dot.transform.position = endPosition;

        // Set position of the linerenderer
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float length){
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);

        return hit;
    }
}
