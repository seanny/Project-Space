using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyType type;
    public EnemyState state;

    public float speed;

    Rigidbody2D rb;
    Collider2D boxCollider;

    SpriteRenderer sprite;

    #region Constructor
    public Enemy(EnemyType aType, float aSpeed)
    {
        type = aType;
        speed = aSpeed;

    }
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        InvokeRepeating("ChangeDirection", 3f, 3f);
    }

    private void FixedUpdate()
    {
        switch (type)
        {
            case EnemyType.Patrol:
                Move(speed);

                break;
            case EnemyType.Shooter:

                break;
        }
            
    }

    //Move The Enemy
    void Move(float speed)
    {
        rb.velocity = new Vector2(speed, rb.velocity.y) ;
    }

    //Change Direction
    void ChangeDirection()
    {
        speed = speed * -1;

        if (speed > 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }

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
    idle,
    aggro,
    dead
}
