using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int Stamina;
    public float MoveSpeed;
    public float RunSpeed;
    public float JumpForce;

    private float CurrentSpeed;
    private float TargetSpeed;
    private float HorizontalInput;

    Rigidbody2D rb;
    SpriteRenderer sp;

    float SpriteWidth;
    float SpriteHeight;

    public int AmountOfRaysToCheckIfGrounded;
    public LayerMask GroundLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        sp = GetComponent<SpriteRenderer>();
        CalculateSpriteBoundaries();
        
    }

    void CalculateSpriteBoundaries()
    {
        SpriteWidth = sp.bounds.extents.x;
        SpriteHeight = sp.bounds.extents.y;
    }
    
    private bool IsGrounded()
    {
        AmountOfRaysToCheckIfGrounded = 2;
        float RayLength = 0.2f;
        Debug.Log("Checking if grounded");
        Vector3 bottomleft = new Vector3(-SpriteWidth, -SpriteHeight, 0);
        float DistanceBetweenRays = 2 * SpriteWidth / AmountOfRaysToCheckIfGrounded;

        for (int i = 0; i <= AmountOfRaysToCheckIfGrounded; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + bottomleft + Vector3.right * (i) * DistanceBetweenRays, Vector2.down, bottomleft.y + RayLength, GroundLayer);
            Debug.DrawRay(transform.position + bottomleft + Vector3.right * (i) * DistanceBetweenRays, Vector2.down * RayLength, Color.red, 9999);
            Debug.Log(bottomleft);
            if (hit)
            {

                Debug.Log("Hitting Ground");
                return true;
            }
        
        }
        Debug.Log("Did not hit ground");
        return false;
    }

    private void FixedUpdate()
    {
        #region HorizontalInput
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.LeftShift)) { TargetSpeed = RunSpeed; }
        else { TargetSpeed = MoveSpeed; }

        rb.velocity = new Vector2(HorizontalInput * TargetSpeed, rb.velocity.y);
        #endregion

        #region Jumping
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = Vector2.up * JumpForce;
        }
        #endregion
    }



}
