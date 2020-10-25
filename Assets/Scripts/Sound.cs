using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Found this concept in a Brackeys tutorial.  It is a Singleton but
/// it really helps to organize
/// https://www.youtube.com/watch?v=6OT43pvUyfY
/// See AudioManger
/// </summary>
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0,1)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}
