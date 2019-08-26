using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteManagement : MonoBehaviour
{
    public Sprite idleSprite;
    public List<Sprite> walkingSprites;
    public Sprite startJumpSprite;
    public Sprite middleJumpSprite;
    public Sprite endJumpSprite;

    private SpriteRenderer m_SpriteRenderer;
    private PlayerCombat m_PlayerCombat;

    private void OnEnable()
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
        InitRenderer();
        SetIdleSprite();
    }

    // Update is called once per frame
    private void Update()
    {
        if (PlayerManager.Instance.movement.IsGrounded() != true)
        {
            m_SpriteRenderer.sprite = middleJumpSprite;
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
    }

    private void SetIdleSprite()
    {
        m_SpriteRenderer.sprite = idleSprite;
    }
}
