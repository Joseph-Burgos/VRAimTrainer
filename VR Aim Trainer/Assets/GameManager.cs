using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //create single instance to be able to call it anywhere
    public static GameManager Instance;

    public bool useLaser = false;
    public bool useVFX;
    public bool useTrail = true;
    public int currentSkinIndex;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        useTrail = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void switchstate()
    {
        useVFX = !useVFX;
    }

    public void toggleLaser()
    {
        useLaser = !useLaser;
    }

    public void toggleTrail()
    {
        useTrail = !useTrail;
    }
}
