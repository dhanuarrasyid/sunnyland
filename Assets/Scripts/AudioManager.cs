using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
	// Use this for initialization
	void Awake () {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.audioClip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
	}
	
    public void Play(string name)
    {
        Debug.Log("playing " + name);
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
