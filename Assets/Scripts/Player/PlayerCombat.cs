using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public delegate void SwitchWeapon();
    public static event SwitchWeapon SwitchingWeapon;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void OnDestroy()
    {
        SwitchingWeapon += SwitchWeaponAttack;
    }
    
    void SwitchWeaponAttack()
    {

    }

    void Attack()
    {

    }

    
}

[HideInInspector]
public enum AttackToDo
{
    melee,
    ranged,
    nothing
}