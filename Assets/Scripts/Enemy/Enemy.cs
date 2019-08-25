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
        InvokeRepeating("ChangeDirection", 3f, 3f);
    }

    private void FixedUpdate()
    {
        switch (type)
        {
            case EnemyType.Patrol:
                Move(speed);

                break;
            case EnemyType.Tank:

                break;
        }
            
    }

    //Move The Enemy
    void Move(float speed)
    {
        rb.velocity = new Vector2(speed, rb.velocity.y) ;

        //rb.MovePosition(transform.position + Vector3.right * speed * Time.fixedDeltaTime);
    }

    //Change Direction
    void ChangeDirection()
    {
        speed = speed * -1;
    }

}

//Type of Enemy
public enum EnemyType
{
    Patrol,
    Tank
}

//Which state te enemy is in
public enum EnemyState
{
    idle,
    aggro,
    dead
}
