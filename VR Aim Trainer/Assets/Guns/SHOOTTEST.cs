using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHOOTTEST : MonoBehaviour
{
    public float range = 100f;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            shoot();
    }

    private void shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            //store target component thats been hit
            Target currentTarget = hit.transform.GetComponent<Target>();
            //check if we hit an object that actually had a target component
            if(currentTarget != null)
            {
                currentTarget.hit();  
            }
        }
        //show debug of where its shooting
        Debug.DrawLine(cam.transform.position, cam.transform.position + cam.transform.forward * 100, Color.green ,3f);
    }
}
