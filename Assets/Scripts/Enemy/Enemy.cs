using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [Header("Player Layer")]
    public LayerMask playerLayer;

    [Header("Enums")]
    public EnemyType type;
    private EnemyState state;

    [Header("Stats")]
    public int health;
    internal int maxHealth;

    [Header("Patrol Variables")]
    public float movSpeed;
    public int changeDirectionTime;

    [Header("Shooter Variables")]
    public int shotSpeed;
    public bool playerSpotted;

    [Header("HitBox Parameters")]
    public Transform attackSpawnPos;
    public Vector2 MeleeAttackParameters;

    Rigidbody2D rb;
    SpriteRenderer sprite;
    internal PlayerCombat playerCombat;
    internal GameObject player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        player = GameManager.instance.player.gameObject;
        
        playerCombat = player.GetComponent<PlayerCombat>();

        maxHealth = health;

        if (type.Equals(EnemyType.Patrol))
        {
            InvokeRepeating("ChangeDirection", changeDirectionTime, changeDirectionTime);
        }

    }

    private void FixedUpdate()
    {
        switch (type)
        {
            case EnemyType.Patrol:
                Move(movSpeed);
                break;

            case EnemyType.Shooter:
                CheckPlayerPosition();
            break;
        }    
    }

    public virtual void ReceiveDamage(int dmgAmount)
    {
        StartCoroutine(ChangeColor());
        health -= dmgAmount;

        if (health <= 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator ChangeColor()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(.2f);
        sprite.color = Color.white;
    }

    IEnumerator Death()
    {
        type = EnemyType.Dead;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    #region Patrol
    //Move The Enemy
    void Move(float speed)
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y) ;
    }

    //Change Direction
    void ChangeDirection()
    {
        movSpeed = movSpeed * -1;

        if (movSpeed > 0)
            sprite.flipX = false;
        else
            sprite.flipX = true;
    }
    #endregion

    #region Shooter
    //Shoot
    public void Shoot()
    {
        GameObject bullet = Pooler.instance.SpawnFromPool("Normal", transform.position, transform.rotation);

        Rigidbody2D projectileRB = bullet.GetComponent<Rigidbody2D>();

        projectileRB.velocity = Vector2.right * shotSpeed;

        Debug.Log(gameObject.name + " is Shooting");

    }

    
     //Check which direction the player should shoot
    public void CheckPlayerPosition()
    {
        if (GameManager.instance.player.transform.position.x < transform.position.x)
        {
            shotSpeed = -Mathf.Abs(shotSpeed);

            sprite.flipX = false;
        }
        else
        {
            shotSpeed = Mathf.Abs(shotSpeed);

            sprite.flipX = true;
        }
            
    }

    //If the player reaches a certain range the enemy starts to shoot
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && type.Equals(EnemyType.Shooter) && !playerSpotted)
        {
            playerSpotted = true;

            InvokeRepeating("Shoot", 0f, 3f);
        }
    }
    #endregion

    #region Melee Attack
    void Attack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(attackSpawnPos.position, MeleeAttackParameters, 0, playerLayer);

        foreach (Collider2D col in hitColliders)
        {
            Debug.Log("Hit: " + col.gameObject.name);
        }
        Debug.Log(gameObject.name + " Attacked!");
    }

    #endregion

    #region FollowPlayer

    public void FollowPlayer()
    {
        Vector2 playerPos = player.transform.position;

        Vector2 movDir = (playerPos - (Vector2)rb.transform.position).normalized;

        Vector2 convertedPos = new Vector2(movDir.x, rb.velocity.y);

        rb.velocity = convertedPos * movSpeed;

        if (Vector2.Distance(playerPos, (Vector2)rb.transform.position) < 5f)
        {
            if (!state.Equals(EnemyState.Aggro))
            {
                state = EnemyState.Aggro;
                InvokeRepeating("Attack", 0f, 3f);
            }
            
        }
        else
        {
            if (IsInvoking("Attack"))
            {
                CancelInvoke("Attack");
            }

            state = EnemyState.Following;
        }
    }

    #endregion

    #region Deal Damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }

        if (collision.gameObject.tag.Equals("Projectile"))
        {
            ReceiveDamage(2);
        }
    }
    #endregion


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(attackSpawnPos.position, new Vector2(attackSpawnPos.position.x + MeleeAttackParameters.x / 2, attackSpawnPos.position.y));
        Gizmos.DrawLine(attackSpawnPos.position, new Vector2(attackSpawnPos.position.x - MeleeAttackParameters.x / 2, attackSpawnPos.position.y));
        Gizmos.DrawLine(attackSpawnPos.position, new Vector2(attackSpawnPos.position.x, MeleeAttackParameters.y / 2 + attackSpawnPos.position.y));
        Gizmos.DrawLine(attackSpawnPos.position, new Vector2(attackSpawnPos.position.x, -MeleeAttackParameters.y / 2 + attackSpawnPos.position.y));
    }

    #region GameFeel

    bool canJump = true;
    public void JumpOnHit()
    {
        if (canJump)
        {
            rb.AddForce(new Vector2(0, 1) * 4f, ForceMode2D.Impulse);
            canJump = false;
            StartCoroutine(EnableJump());
        }
    }

    IEnumerator EnableJump()
    {
        yield return new WaitForSeconds(2f);
        canJump = true;
    }
    #endregion

}

//Type of Enemy
public enum EnemyType
{
    Patrol,
    Shooter,
    Morpher,
    Dead
}

//Which state te enemy is in
public enum EnemyState
{
    Idle,
    Following,
    Aggro,
    Dead
}
