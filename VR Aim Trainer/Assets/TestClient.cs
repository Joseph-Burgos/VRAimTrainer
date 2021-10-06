using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClient : MonoBehaviour
{
    //public ScoreManager sm;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting the test client");
        
    }

    // Update is called once per frame
    void Update()
    {
        //if press up, save a score
        if (Input.GetKeyDown("up"))
        {
            ScoreManager.AddToScore(100);
            Debug.Log("Input detected");
        }

    }
}
