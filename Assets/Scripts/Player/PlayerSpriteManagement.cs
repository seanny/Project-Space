using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteManagement : MonoBehaviour
{
    public int currentIdleSprite = 0;

    private SpriteRenderer m_SpriteRenderer;
    private PlayerCombat m_PlayerCombat;
    private PlayerMovement m_PlayerMovement;
    Animator anim;
    Rigidbody2D rb;
    bool jumped = false;

    public Animator SwingSprite;

    


    private void OnEnable()
    {

    }

    private void Start()
    {
        InitRenderer();
        SetIdleSprite();

        PlayerCombat.PlayerAttacking += AttackAnim;
    }

    void AttackAnim()
    {
        SwingSprite.SetTrigger("Swing");
        anim.SetTrigger("Attack");
    }

    private void Update()
    {
        if (rb.velocity.x != 0 && Input.GetAxisRaw("Horizontal") != 0) { anim.SetBool("Walking", true); }
        else { anim.SetBool("Walking", false); }

        if (!m_PlayerMovement.IsGrounded() && Input.GetKey(KeyCode.Space) && !jumped)
        {
            jumped = true;
            anim.Play("Jump");
            anim.SetBool("InAir", true);
            //Debug.Log("Jumped");
        }
        else if (m_PlayerMovement.IsGrounded())
        {
            jumped = false;
            anim.SetBool("InAir", false);
            anim.ResetTrigger("Jump");

        }
        /*
        if (Input.GetMouseButtonDown(0) && m_PlayerCombat.attacktodo.Equals(AttackToDo.melee))
        {
            if (m_PlayerCombat.timebetweenattacks < 0)
            {
            SwingSprite.SetTrigger("Swing");
            anim.SetTrigger("Attack");
            }           
        }
            */

    }


    private void InitRenderer()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_PlayerCombat = GetComponent<PlayerCombat>();
        m_PlayerMovement = GetComponent<PlayerMovement>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        PlayerCombat.SwitchingWeapon += SwitchWeaponSprite;
    }

    void SwitchWeaponSprite()
    {

        switch (m_PlayerCombat.attacktodo)
        {
            case AttackToDo.melee:
                currentIdleSprite = 1;
                break;
            case AttackToDo.ranged:
                currentIdleSprite = 2;
                break;
            case AttackToDo.nothing:
                currentIdleSprite = 0;
                break;
        }

        anim.SetFloat("Idle", currentIdleSprite);
    }

    private void SetIdleSprite()
    {
        //m_SpriteRenderer.sprite = idleSprite;
    }
}
