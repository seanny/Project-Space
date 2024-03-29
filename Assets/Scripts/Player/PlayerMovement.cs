﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    
    public int Stamina;
    public float MoveSpeed;
    public float RunSpeed;
    public float JumpForce;
    public bool jumped;

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

    [HideInInspector]
    public bool facingRight = true;

    public int AmountOfRaysToCheckIfGrounded;
    public LayerMask GroundLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        sp = GetComponentInChildren<SpriteRenderer>();
        CalculateSpriteBoundaries();

        if (GameManager.instance != null)
        {
            GameManager.instance.player = gameObject;
            transform.parent = GameManager.instance.transform;
        }
        
    }

    void CalculateSpriteBoundaries()
    {
        SpriteWidth = sp.bounds.extents.x;
        SpriteHeight = sp.bounds.extents.y;
    }

    public bool IsGrounded()
    {
        int FunctionAmountOfRaysToCheckIfGrounded = AmountOfRaysToCheckIfGrounded - 1;
        float RayLength = 0.3f;
        //Debug.Log("Checking if grounded");
        Vector3 bottomleft = new Vector3(-SpriteWidth / 2, -SpriteHeight, 0);
        float DistanceBetweenRays = 2 * SpriteWidth / AmountOfRaysToCheckIfGrounded;

        for (int i = 0; i <= AmountOfRaysToCheckIfGrounded - 1; i++)
        {
            Ray2D ray = new Ray2D
            {
                origin = new Vector2(transform.position.x + bottomleft.x + (i) * DistanceBetweenRays, transform.position.y)
            };
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.down, RayLength, GroundLayer);
            Debug.DrawRay(ray.origin, Vector2.down * RayLength, Color.red, 5);
            //Debug.Log(ray.origin);
            //Debug.Log(bottomleft);
            if (hit)
            { //Debug.Log("Hitting Ground");
                return true;
            }
        //Debug.Log("Did not hit ground");
        }
        return false;
    }

    private float groundedtime;
    private readonly float maxgroundedtime = 0.1f;

    private void Update()
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PressedJumpTime = JustPressedJumpTime;

        }
        if (PressedJumpTime >= 0) { PressedJumpTime -= Time.fixedDeltaTime; }
        if (groundedtime >= 0) { groundedtime -= Time.fixedDeltaTime; }

        if (rb.velocity.y > 0)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * JumpFallMultiplier);
            }
        }

        if (IsGrounded())
        {
            groundedtime = maxgroundedtime;
        }

        if (PressedJumpTime > 0)
        {
            if (groundedtime > 0)
            {
                if (Dialog.instance.TextBox.GetComponent<Image>().enabled  != true) { AudioManager.instance.PlaySound("Jump"); }
                PressedJumpTime = 0;
                groundedtime = 0;
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            }
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
