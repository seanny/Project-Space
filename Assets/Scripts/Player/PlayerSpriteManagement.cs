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

    private void OnEnable()
    {

    }

    private void Start()
    {
        InitRenderer();
        SetIdleSprite();
    }

    private void Update()
    {
        if (rb.velocity.x != 0) { anim.SetBool("Walking", true); }
        else { anim.SetBool("Walking", false); }

        if (!m_PlayerMovement.IsGrounded())
        {
            anim.SetTrigger("Jump");
            anim.SetBool("InAir", true);
        }
        else
        {
            anim.SetBool("InAir", false);
            anim.ResetTrigger("Jump");
        }
            

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
