using UnityEngine;

[System.Serializable]
public class Sound {
    public string name;
    public AudioClip audioClip;

    [HideInInspector]
    public AudioSource source;

    [Range(0f,1f)]
    public float volume = .5f;

    [Range(0.1f, 3f)]
    public float pitch = 1f;

}
