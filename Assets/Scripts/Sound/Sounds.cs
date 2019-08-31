using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds
{

    public string soundName;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    public float pitch;
}
