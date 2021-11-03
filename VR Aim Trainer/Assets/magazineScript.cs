using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magazineScript : MonoBehaviour
{
    public int ammoCount = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Magazine collided with: " + collision.gameObject.name);

        // FIXME
        // set the ammo of the gun to water the ammo count is.
        if(collision.gameObject.name == "Magazine Collider")
        {
        }
    }
}
