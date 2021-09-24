using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer; 

    public void IncreaseVolume(){
        float value;
        bool result = audioMixer.GetFloat("MasterVolume", out value);
        if(result){
            if (value < 20f){
                audioMixer.SetFloat("MasterVolume", value + 5f);
            }
        }        
    }

    public void DecreaseVolume(){
        float value;
        bool result = audioMixer.GetFloat("MasterVolume", out value);
        if(result){
            if (value > -80f){
                audioMixer.SetFloat("MasterVolume", value - 5f);
                Debug.Log(GetVolume());
            }
        }
    }

    private float GetVolume(){
        float value;
        audioMixer.GetFloat("MasterVolume", out value);
        return value;
    }
}
