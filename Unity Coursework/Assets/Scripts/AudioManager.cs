using System;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    // this class is based of code from the audio manager tutorial on youtube by Brackeys
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Sound[] sounds;
    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);

            return;
        }
            DontDestroyOnLoad(gameObject);

            foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        
        Play("Theme");
    }


    // Update is called once per frame
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);


        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.Stop();
    }
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.Pause();
    }
    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.UnPause();
    }
    
    
}

