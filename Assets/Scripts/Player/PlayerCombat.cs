using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public delegate void SwitchWeapon();
    public static event SwitchWeapon SwitchingWeapon;

    public AttackToDo attacktodo = AttackToDo.nothing;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Switching Weapon");
            SwitchingWeapon?.Invoke();
        }
    }
    

    void OnDestroy()
    {
        SwitchingWeapon -= SwitchWeaponAttack;
    }

    private void OnEnable()
    {
        SwitchingWeapon += SwitchWeaponAttack;
    }

    void SwitchWeaponAttack()
    {
        switch (attacktodo)
        {
            case AttackToDo.melee: attacktodo = AttackToDo.ranged; break;

            case AttackToDo.ranged: attacktodo = AttackToDo.nothing; break;

            case AttackToDo.nothing: attacktodo = AttackToDo.melee; break;
        }
        
    }

    
    
}

public enum AttackToDo
{
    melee,
    ranged,
    nothing
}