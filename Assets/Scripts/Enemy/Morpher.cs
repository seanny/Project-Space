using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morpher : Enemy
{
    [Header("Mode")]
    public MorpherMode mode;

    /*void Start()
    {
        mode = MorpherMode.Melee;
    }*/
    private void Update()
    {
        CheckPlayerPosition();

        switch (mode)
        {
            case MorpherMode.Ranged:
                break;
            case MorpherMode.Melee:
                break;
            default:
                break;
        }

        if (health < maxHealth / 2 && !mode.Equals(MorpherMode.Ranged))
        {
            mode = MorpherMode.Ranged;
            InvokeRepeating("Shoot", 0f, 3f);
        }
    }

    public override void ReceiveDamage(int dmgAmount)
    {
        base.ReceiveDamage(dmgAmount);

        if (health < health/2 && !mode.Equals(MorpherMode.Ranged))
        {
            mode = MorpherMode.Ranged;
            InvokeRepeating("Shoot", 0f, 3f);
        }
    }

}

public enum MorpherMode
{
    Melee,
    Ranged
}


