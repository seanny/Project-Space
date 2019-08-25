using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyType type;
    public EnemyState state;

    public float speed;
    public bool inRadius;

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

    private void Update()
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
        Vector3 dir = new Vector2(1, 0);

        rb.MovePosition(transform.position + dir * speed * Time.fixedDeltaTime);
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
