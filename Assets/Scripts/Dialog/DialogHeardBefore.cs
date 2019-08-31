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
    [HideInInspector]
    public bool captain;
    [HideInInspector]
    public bool shiptwo;

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
            case "captain":
                if (!overRide) return captain;
                else
                {
                    captain = true;
                    return captain;
                }
            case "shiptwo":
                if (!overRide) return shiptwo;
                else
                {
                    shiptwo = true;
                    return shiptwo;
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
