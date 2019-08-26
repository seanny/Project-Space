using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

<<<<<<< HEAD
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {

    }
=======
>>>>>>> 5d9d67edf074d8e689d72ecc30c8a1ab3cca69a6
}

[HideInInspector] public enum AttackToDo
{
    melee,
    ranged,
    nothing
}
    
