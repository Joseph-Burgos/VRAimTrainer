using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public AudioMixerGroup m_MixerGroup;

    void Awake(){
        foreach (Sound s in sounds){

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = m_MixerGroup;
        }
    }

    public void Play (string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
