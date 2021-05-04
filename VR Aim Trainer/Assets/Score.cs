using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score = 0;
    public int time = 10;
    public string name = "test";


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            DataBase.Savedata(this);
            Debug.Log("Saved");
        }
        if (Input.GetKeyDown("down"))
        {
            Debug.Log("loaded");
        }
    }

      
}
