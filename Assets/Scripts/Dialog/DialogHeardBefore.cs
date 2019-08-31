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
    [HideInInspector]
    public bool sword = false;
    [HideInInspector]
    public bool asteroid;
    [HideInInspector]
    public bool ship;

    public bool GetBoolValue(string boolname, bool overRide)
    {
        switch (boolname)
        {
            case "intro":
                if (!overRide) return intro;
                else
                {
                    intro = true;
                    return intro;
                }
            case "sword":
                if (!overRide) return sword;
                else
                {
                    sword = true;
                    return sword;
                }
            case "asteroid":
                if (!overRide) return asteroid;
                else
                {
                    asteroid = true;
                    return asteroid;
                }
            case "ship":
                if (!overRide) return ship;
                else
                {
                    ship = true;
                    return ship;
                }
            default:
                return false;
        }
    }
   
    
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
    public bool intro; */
}
