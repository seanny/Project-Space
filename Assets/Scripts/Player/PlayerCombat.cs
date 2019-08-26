using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public delegate void SwitchWeapon();
    public static event SwitchWeapon SwitchingWeapon;

    public AttackToDo attacktodo = AttackToDo.nothing;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }

    void SwitchWeaponToAttack()
    {
        SwitchingWeapon?.Invoke();
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

            case AttackToDo.ranged: attacktodo = AttackToDo.melee; break;

            case AttackToDo.nothing: Debug.Log("Doing Nothing"); break;
        }
    }

    
    
}

public enum AttackToDo
{
    melee,
    ranged,
    nothing
}