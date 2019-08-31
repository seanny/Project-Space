using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;


    private void Awake()
    {
        foreach (Sounds s in sounds)
        {
            gameObject.AddComponent<AudioSource>();
        }
    }
}
