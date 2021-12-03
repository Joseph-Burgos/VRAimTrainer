using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    public LineRenderer trail;
    public Transform pos1;
    // Start is called before the first frame update

    void Start()
    {
        //ensure linetrace only has 2 positions
        trail.positionCount = 2;
    }
    public void shootTrail()
    {
        //if the trail is on, turn it off to avoid weird visuals
        if (this.gameObject.activeSelf)
        {
            CancelInvoke("setFalse");
            this.gameObject.SetActive(false);
        }
        //make trail visible
        this.gameObject.SetActive(true);
        //update position of start of trail
        trail.SetPosition(0, pos1.position);
        //get position of end of trail
        Vector3 pos2 = pos1.position + pos1.forward * 20;
        //set position of end of trail
        trail.SetPosition(1, pos2);
        //disable trail after a second
        Invoke("setFalse", 1f);
    }
    //function to disable trail to call in invoke method
    void setFalse()
    {
        this.gameObject.SetActive(false);
    }
}
