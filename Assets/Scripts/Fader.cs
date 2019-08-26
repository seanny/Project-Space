using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public static Fader instance;

    Animator anim;


    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    public void FadeOut()
    {
        anim.SetBool("FadeOut", true);
    }

    public void FadeIn()
    {

        anim.SetBool("FadeOut", false);
    }
}
