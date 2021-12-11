using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton m_Instance;

    // Update is called once per frame
    void Awake()
    {
        if (m_Instance == null){
            m_Instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
