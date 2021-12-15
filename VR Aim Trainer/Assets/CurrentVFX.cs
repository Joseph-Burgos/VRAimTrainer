using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentVFX : MonoBehaviour
{
    [SerializeField]
    private GameObject VFXText = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.useVFX)
        {
            VFXText.GetComponent<TMPro.TextMeshPro>().text = "ON";
        }
        else
        {
            VFXText.GetComponent<TMPro.TextMeshPro>().text = "OFF";
        }
    }
}
