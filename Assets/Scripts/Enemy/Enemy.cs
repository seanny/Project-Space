using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enums")]
    public EnemyType type;
    public EnemyState state;

    [Header("Patrol Variables")]
    public float movSpeed;

    [Header("Shooter Variables")]
    public int shotSpeed;
    public bool playerSpotted;

    Rigidbody2D rb;
    SpriteRenderer sprite;

    #region Constructor
    public Enemy(EnemyType aType, float aSpeed)
    {
        type = aType;
        movSpeed = aSpeed;

    }
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        if (type.Equals(EnemyType.Patrol))
        {
            InvokeRepeating("ChangeDirection", 3f, 3f);
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

    #region Patrol
    //Move The Enemy
    void Move(float speed)
    {
        rb.velocity = new Vector2(speed, rb.velocity.y) ;
    }

    //Change Direction
    void ChangeDirection()
    {
        movSpeed = movSpeed * -1;

        if (movSpeed > 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }
    #endregion

    #region Shooter
    //Shoot
    void Shoot()
    {
        GameObject bullet = Pooler.instance.SpawnFromPool("Normal", transform.position, transform.rotation);

        Rigidbody2D projectileRB = bullet.GetComponent<Rigidbody2D>();

        projectileRB.velocity = Vector2.right * shotSpeed;

    }

    
     //Check which direction the player should shoot
    void CheckPlayerPosition()
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

}

//Type of Enemy
public enum EnemyType
{
    Patrol,
    Shooter
}

//Which state te enemy is in
public enum EnemyState
{
    Idle,
    Aggro,
    Dead
}
