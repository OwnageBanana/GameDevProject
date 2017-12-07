//Author: Adam Mills

using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    public List<Sound> sounds;

    public static AudioManager instance;

	// Use this for initialization
	void Awake () {


        //innitalizing the sound objects with AudioSourceObjects
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
	}


    void Start()
    {
        play("bgSound1");
    }

    /// <summary>
    /// plays the sound clip by the name provided
    /// </summary>
    /// <param name="name">name specifying the sound clip</param>
    public void play(string name)
    {
        //find sound in list
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null)
        {
            //if not found, debug log the name tried
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        //if the sound isn't already playing, play sound
        if (!s.source.isPlaying)
            s.source.Play();
    }

    /// <summary>
    /// stops the sound specified from playing
    /// </summary>
    /// <param name="name">name specifying the sound to stop playing</param>
    public void stop(string name)
    {
        //finding the sound
        Sound s = sounds.Find(sound => sound.name == name);

        //if the sound isn't found, log attempted name
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        //stop the sound
        s.source.Stop();
    }
}
