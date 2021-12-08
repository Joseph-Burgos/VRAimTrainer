using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magazineScript : MonoBehaviour
{
    public int ammoCount = 8;
    public bool newlySpawned = false;

    public void Start()
    {
        Transform skins = this.transform.GetChild(0);//MagSkins
        for(int i = 0; i < skins.childCount; i++)
        {
            if (i == GameManager.Instance.currentSkinIndex)
            {
                skins.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                skins.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
