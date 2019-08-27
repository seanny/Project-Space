using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morpher : Enemy
{
    [Header("Mode")]
    public MorpherMode mode;

    private void FixedUpdate()
    {
        CheckPlayerPosition();

        switch (mode)
        {
            case MorpherMode.Ranged:
                break;
            case MorpherMode.Melee:
                FollowPlayer();
                break;
            default:
                break;
        }
    }

    public override void ReceiveDamage(int dmgAmount)
    {
        base.ReceiveDamage(dmgAmount);

        if (health < maxHealth/2 && !mode.Equals(MorpherMode.Ranged))
        {
            mode = MorpherMode.Ranged;
            InvokeRepeating("Shoot", 0f, 3f);
        }

        Debug.Log("Damaged");
    }

}

public enum MorpherMode
{
    Melee,
    Ranged
}


