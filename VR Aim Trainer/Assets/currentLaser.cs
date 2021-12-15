using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentLaser : MonoBehaviour
{
    [SerializeField]
    private GameObject LaserText = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.useLaser)
        {
            LaserText.GetComponent<TMPro.TextMeshPro>().text = "ON";
        }
        else
        {
            LaserText.GetComponent<TMPro.TextMeshPro>().text = "OFF";
        }

    }
}
