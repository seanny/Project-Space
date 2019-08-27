using UnityEngine;
using UnityEditor;

public class PlayerCombat : MonoBehaviour
{

    public delegate void SwitchWeapon();
    public static event SwitchWeapon SwitchingWeapon;

    public AttackToDo attacktodo = AttackToDo.nothing;

    public Transform MeleeAttackSpawnPos;
    public GameObject RangedAttackSpawnPos;
    public Vector2 MeleeAttackParameters;
    public LayerMask EnemyLayer;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (attacktodo)
            {
                case AttackToDo.melee: MeleeAttack(); break;

                case AttackToDo.ranged: RangedAttack(); break;

                case AttackToDo.nothing: break;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Switching Weapon");
            SwitchingWeapon?.Invoke();
        }
    }

    void MeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(MeleeAttackSpawnPos.position, MeleeAttackParameters, 0, EnemyLayer );

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hitting enemy");
        }

        Debug.Log("Swinging");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(MeleeAttackSpawnPos.position, new Vector2(MeleeAttackSpawnPos.position.x + MeleeAttackParameters.x / 2, MeleeAttackSpawnPos.position.y));
        Gizmos.DrawLine(MeleeAttackSpawnPos.position, new Vector2(MeleeAttackSpawnPos.position.x - MeleeAttackParameters.x / 2, MeleeAttackSpawnPos.position.y));
        Gizmos.DrawLine(MeleeAttackSpawnPos.position, new Vector2(MeleeAttackSpawnPos.position.x, MeleeAttackParameters.y / 2 + MeleeAttackSpawnPos.position.y));
        Gizmos.DrawLine(MeleeAttackSpawnPos.position, new Vector2(MeleeAttackSpawnPos.position.x, -MeleeAttackParameters.y / 2 + MeleeAttackSpawnPos.position.y));
    }

    void RangedAttack()
    {

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