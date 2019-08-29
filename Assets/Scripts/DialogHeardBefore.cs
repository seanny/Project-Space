using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHeardBefore : MonoBehaviour
{
    public static DialogHeardBefore instance;
    private void Awake()
    {
        instance = this;
    }
    [HideInInspector]
    public bool intro = false;
    //[HideInInspector]
    public bool sword = false;
    /*[HideInInspector]
    public bool intro;
    [HideInInspector]
    public bool intro;
    [HideInInspector]
    public bool intro;
    [HideInInspector]
    public bool intro;
    [HideInInspector]
    public bool intro;
    [HideInInspector]
    public bool intro;
    [HideInInspector]
    public bool intro;
    [HideInInspector]
    public bool intro;
    [HideInInspector]
    public bool intro;
    [HideInInspector]
    public bool intro; */
}
