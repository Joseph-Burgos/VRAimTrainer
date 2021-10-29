using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    private List<GameObject> m_Skins = new List<GameObject>();
    private string m_SkinPref;
    private int indexOfCurrentSkin;

    void Awake(){
        Transform glockSkins = GameObject.Find("Skins").transform;

        // find the prefered skin, if no prefered returns default 
        m_SkinPref = PlayerPrefs.GetString("GlockSkin", "G26");


        bool skinIsActive = false;
        int children = glockSkins.childCount;
        for (int i = 0; i < children; ++i){
            GameObject skinObject = glockSkins.GetChild(i).gameObject;
            // skinObject.SetActive(true);

            m_Skins.Add(skinObject);

            if (skinObject.name.Equals(m_SkinPref)){
                skinObject.SetActive(true);
                skinIsActive = true;
            }
            else{
                skinObject.SetActive(false);
            }
        }

        if (!skinIsActive){
            m_Skins[0].SetActive(true);
        }

    }

    public void SetNextSkin(){
        // set the current skin as inactive
        m_Skins[indexOfCurrentSkin].SetActive(false);

        // update the new index to the next skinin the list
        indexOfCurrentSkin += 1;
        // check if the new index is within range of the list of skins, if not wrap around
        if (indexOfCurrentSkin >= m_Skins.Count){
            indexOfCurrentSkin = 0;
        }

        // sets the new skin to active
        m_Skins[indexOfCurrentSkin].SetActive(true);

        m_SkinPref = m_Skins[indexOfCurrentSkin].name;
        PlayerPrefs.SetString("GlockSkin", m_SkinPref);
        PlayerPrefs.Save();
    }

    public void SetPreviouSkin(){
        // set the current skin as inactive
        m_Skins[indexOfCurrentSkin].SetActive(false);

        // update the new index to the previous skin in the list
        indexOfCurrentSkin -= 1;
        // check if the new index is within range of the list of skins, if not wrap around
        if (indexOfCurrentSkin < 0){
            indexOfCurrentSkin = m_Skins.Count - 1;
        }

        // sets the new skin to active
        m_Skins[indexOfCurrentSkin].SetActive(true);

        m_SkinPref = m_Skins[indexOfCurrentSkin].name;
        PlayerPrefs.SetString("GlockSkin", m_SkinPref);
        PlayerPrefs.Save();
    }

    public int GetIndexOfCurrentSkin(){
        return indexOfCurrentSkin
    }
}
