using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteManagement : MonoBehaviour
{
    public int currentIdleSprite = 0;
    /*public List<Sprite> walkingSprites;
    public Sprite startJumpSprite;
    public Sprite middleJumpSprite;
    public Sprite endJumpSprite;*/

    private SpriteRenderer m_SpriteRenderer;
    private PlayerCombat m_PlayerCombat;
    Animator anim;

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
        if (PlayerManager.Instance.movement.IsGrounded() != true)
        {
            //m_SpriteRenderer.sprite = middleJumpSprite;
        }
        else
        {
            SetIdleSprite();
        }
    }

    private void InitRenderer()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_PlayerCombat = GetComponent<PlayerCombat>();
        anim = GetComponentInChildren<Animator>();

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
