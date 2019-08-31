using UnityEngine;
using UnityEditor;

public class PlayerCombat : MonoBehaviour
{

    public delegate void SwitchWeapon();
    public static event SwitchWeapon SwitchingWeapon;


    public delegate void PlayerAttack();
    public static event PlayerAttack PlayerAttacking;

    public AttackToDo attacktodo = AttackToDo.nothing;

    public Transform MeleeAttackSpawnPos;
    public Transform RangedAttackSpawnPos;
    public Vector2 MeleeAttackParameters;
    public LayerMask EnemyLayer;
    public GameObject Hud;
    private Animator GunHud;
    private Animator SwordHud;

    public float timebetweenattacks = 0.4f;
    private float starttimebetweenattacks = 0.4f;

    public float timeBetweenSword = 1.2f;
    private float startTimebetweenSword= 1.2f;

    [Header("Projectile")]
    public float shotSpeed;

    public int damage;

    private void Start()
    {
        starttimebetweenattacks = timebetweenattacks;
    }

    private void Update()
    {
        timebetweenattacks -= Time.deltaTime;
        timeBetweenSword -= Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            switch (attacktodo)
            {
                case AttackToDo.melee:
                    {
                        if (timebetweenattacks < 0)
                        {
                            MeleeAttack();
                            timebetweenattacks = starttimebetweenattacks;
                            PlayerAttacking?.Invoke();
                        }
                        break;
                    }

                case AttackToDo.ranged:

                    if (timeBetweenSword < 0)
                    {
                        RangedAttack();
                        PlayerAttacking?.Invoke();
                        timeBetweenSword = startTimebetweenSword;
                    }
                    
                    break;

                case AttackToDo.nothing: break;
            }
        }
       
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Switching weapon");
            SwitchingWeapon();
        }
    }

    private bool InitializeHud;
    private void LateUpdate()
    {

        if (!GameManager.instance.player.GetComponent<PlayerMovement>().facingRight)
            shotSpeed = -Mathf.Abs(shotSpeed);
        else
            shotSpeed = Mathf.Abs(shotSpeed);
        Hud = GameObject.FindGameObjectWithTag("HUD");
        if (!InitializeHud)
        {
            GunHud = Hud.transform.GetChild(0).GetComponent<Animator>();
            SwordHud = Hud.transform.GetChild(1).GetComponent<Animator>();
        }
    }

    void MeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(MeleeAttackSpawnPos.position, MeleeAttackParameters, 0, EnemyLayer );

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hitting enemy");
            enemy.GetComponent<Enemy>().ReceiveDamage(damage);
            enemy.GetComponent<Enemy>().JumpOnHit();
        }

        Debug.Log("Swinging");
    }

    void RangedAttack()
    {
        GameObject obj = Pooler.instance.SpawnFromPool("PlayerBullet", RangedAttackSpawnPos.position, Quaternion.identity);

        Rigidbody2D projectileRB = obj.GetComponent<Rigidbody2D>();

        projectileRB.velocity = Vector2.right * shotSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(MeleeAttackSpawnPos.position, new Vector2(MeleeAttackSpawnPos.position.x + MeleeAttackParameters.x / 2, MeleeAttackSpawnPos.position.y));
        Gizmos.DrawLine(MeleeAttackSpawnPos.position, new Vector2(MeleeAttackSpawnPos.position.x - MeleeAttackParameters.x / 2, MeleeAttackSpawnPos.position.y));
        Gizmos.DrawLine(MeleeAttackSpawnPos.position, new Vector2(MeleeAttackSpawnPos.position.x, MeleeAttackParameters.y / 2 + MeleeAttackSpawnPos.position.y));
        Gizmos.DrawLine(MeleeAttackSpawnPos.position, new Vector2(MeleeAttackSpawnPos.position.x, -MeleeAttackParameters.y / 2 + MeleeAttackSpawnPos.position.y));
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
            case AttackToDo.melee:
                if (Dialog.instance.ContinueButton.activeInHierarchy != true && DialogHeardBefore.instance.sword == true)
                {
                    attacktodo = AttackToDo.ranged;
                    SwordHud.SetBool("Opened", true);
                }
                else if (Dialog.instance.ContinueButton.activeInHierarchy != true) attacktodo = AttackToDo.nothing;
                GunHud.SetBool("Opened", false);
                break;

            case AttackToDo.ranged:
                attacktodo = AttackToDo.nothing;
                SwordHud.SetBool("Opened", false);
                break;

            case AttackToDo.nothing:
                if (DialogHeardBefore.instance.intro == true && Dialog.instance.ContinueButton.activeInHierarchy != true)
                {
                    attacktodo = AttackToDo.melee;
                    GunHud.SetBool("Opened", true);
                }
                break;
        }
        //Debug.Log("Switching weapon");
        
    }
    
}

public enum AttackToDo
{
    melee,
    ranged,
    nothing
}