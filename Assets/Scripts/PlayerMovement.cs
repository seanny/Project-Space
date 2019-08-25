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

    public float JustPressedJumpTime;
    private float PressedJumpTime;
    [Range(0,1)]public float JumpFallMultiplier;

    Rigidbody2D rb;
    SpriteRenderer sp;

    float SpriteWidth;
    float SpriteHeight;

    bool facingRight = false;

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
        int FunctionAmountOfRaysToCheckIfGrounded = AmountOfRaysToCheckIfGrounded -1;
        float RayLength = 0.1f;
        //Debug.Log("Checking if grounded");
        Vector3 bottomleft = new Vector3(-SpriteWidth, -SpriteHeight, 0);
        float DistanceBetweenRays = 2 * SpriteWidth / AmountOfRaysToCheckIfGrounded;
        
        for (int i = 0; i <= AmountOfRaysToCheckIfGrounded; i++)
        {
            Ray2D ray = new Ray2D();
            ray.origin = transform.position + bottomleft + Vector3.right * (i) * DistanceBetweenRays;

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.down, bottomleft.y + RayLength, GroundLayer);
            Debug.DrawRay(ray.origin, Vector2.down * RayLength, Color.red, 5);

            //bnm Debug.Log(bottomleft);

            if (hit)
            {//Debug.Log("Hitting Ground");
                return true;
            }
        }//Debug.Log("Did not hit ground");
        return false;
    }

    private void FixedUpdate()
    {
        #region HorizontalInput
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.LeftShift)) { TargetSpeed = RunSpeed; }
        else { TargetSpeed = MoveSpeed; }

        rb.velocity = new Vector2(HorizontalInput * TargetSpeed, rb.velocity.y);

        if (facingRight == false && HorizontalInput > 0 || facingRight == true && HorizontalInput < 0)
        {
            FlipDirections();
        }
        #endregion

        #region Jumping
        if (Input.GetKeyDown(KeyCode.Space)) { PressedJumpTime = JustPressedJumpTime; }
        if (PressedJumpTime >= 0) { PressedJumpTime -= Time.fixedDeltaTime; }
        
        if (rb.velocity.y > 0)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * JumpFallMultiplier);
            }
        }

        if ((PressedJumpTime >= 0) && IsGrounded())
        {
            PressedJumpTime = 0f;
            rb.velocity = Vector2.up * JumpForce;
        }
        #endregion
    }

    void FlipDirections()
    {
        facingRight = !facingRight;
        Vector3 FlipScale = transform.localScale;
        FlipScale.x *= -1;
        transform.localScale = FlipScale;
    }


}
