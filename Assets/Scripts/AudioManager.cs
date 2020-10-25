using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    // Start is called before the first frame update
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
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        // Play("Theme Music");
    }

    // Update is called once per frame
    public void Play(string soundName)
    {
        var targetSound = Array.Find(sounds, sound => sound.name == soundName);
        if (targetSound == null)
        {
            Debug.Log($"We did not find sound: {soundName}");
            return;
        }
        targetSound.source.Play();
    }
}
