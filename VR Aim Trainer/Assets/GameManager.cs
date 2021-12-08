using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //create single instance to be able to call it anywhere
    public static GameManager Instance;

    public bool useVFX;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void switchstate()
    {
        useVFX = !useVFX;
    }
}
