using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/// <summary>
/// Found this concept in a Brackeys tutorial.  It is a Singleton but
/// it really helps to organize
/// https://www.youtube.com/watch?v=6OT43pvUyfY
/// </summary>
public class AudioManager : MonoBehaviour
{
    // Allows us to have Audio manager prefab in each scene, but keep it to the original instance.
    // thus theme music will play across scenes.
    public static AudioManager instance;

    [SerializeField] private Sound[] sounds;
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    
    
    
    void Awake()
    {
        // @TODO is there a better singleton pattern???
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
        
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.outputAudioMixerGroup = audioMixerGroup;
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }
    
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        Play("Menu Music");
    }

    public void StopPlay(string soundName)
    {
        var targetSound = GetTargetSound(soundName);
        if (targetSound == null)
        {
            Debug.Log($"Asked to stop unknown sound: {soundName}");
            return;
        }
        targetSound.source.Stop();
    }

    public void Play(string soundName)
    {
        var targetSound = GetTargetSound(soundName);
        if (targetSound == null)
        {
            Debug.Log($"We did not find sound: {soundName}");
            return;
        }
        targetSound.source.Play();
    }

    private Sound GetTargetSound(string soundName)
    {
        var targetSound = Array.Find(sounds, sound => sound.name == soundName);
        return targetSound;
    }
}
