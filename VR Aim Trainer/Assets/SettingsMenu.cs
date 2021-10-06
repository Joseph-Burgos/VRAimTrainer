using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    private string m_MasterVolume = "MasterVolume";
    private float volumeValue;

    void Start(){
        SetVolume( PlayerPrefs.GetFloat(m_MasterVolume, 0) );
    }

    private void SaveAudioPref(float volume){
        PlayerPrefs.SetFloat(m_MasterVolume, volume);
        PlayerPrefs.Save();
    }

    public void IncreaseVolume(){ 
        if (volumeValue < 20f){
            SetVolume(volumeValue + 5f);
            Debug.Log(GetVolume());
        }
    }

    public void DecreaseVolume(){
        if (volumeValue > -80f){
            SetVolume(volumeValue - 5f);
            Debug.Log(GetVolume());
        }
    }

    private float GetVolume(){
        float value;
        audioMixer.GetFloat(m_MasterVolume, out value);
        return value;
    }

    private void SetVolume(float volume){
        volumeValue = volume;

        audioMixer.SetFloat(m_MasterVolume, volume);

        float value;
        audioMixer.GetFloat(m_MasterVolume, out value);
        Debug.Log("Set Volume value: " + value);

        SaveAudioPref(volume);
    }
}
