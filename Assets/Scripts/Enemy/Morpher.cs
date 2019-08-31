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
        
        if (health < maxHealth/2 && !mode.Equals(MorpherMode.Melee) && playerCombat.attacktodo.Equals(AttackToDo.melee))
        {
            mode = MorpherMode.Melee;
            InvokeRepeating("Shoot", 0f, 3f);

            base.ReceiveDamage(dmgAmount);
        }

        if (mode.Equals(MorpherMode.Melee) && playerCombat.attacktodo.Equals(AttackToDo.ranged))
        {
            base.ReceiveDamage(dmgAmount);
        }
    }

}

public enum MorpherMode
{
    Ranged,
    Melee
}


