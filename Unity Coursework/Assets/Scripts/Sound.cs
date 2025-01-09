using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sound 
{
    // this class is based of code from the audio manager tutorial on youtube by Brackeys
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;
    public bool loop;
}
