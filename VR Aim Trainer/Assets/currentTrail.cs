using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentTrail : MonoBehaviour
{
    [SerializeField]
    private GameObject TrailText = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.useTrail)
        {
            TrailText.GetComponent<TMPro.TextMeshPro>().text = "ON";
        }
        else
        {
            TrailText.GetComponent<TMPro.TextMeshPro>().text = "OFF";
        }
    }
}
