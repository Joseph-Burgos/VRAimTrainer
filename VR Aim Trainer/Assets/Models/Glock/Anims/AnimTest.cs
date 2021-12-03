using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("TEST1Key"))
        {
            //get this game object's animator script and play fire anim
            this.gameObject.GetComponent<Animator>().Play("Fire");
            //Debug.Log("play");
        }
    }
}
