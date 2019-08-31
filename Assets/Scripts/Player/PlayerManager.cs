using System;
using UnityEngine;

/// <summary>
/// Manager class which will be used for accessing player scripts.
/// </summary>
public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager Instance;
    
    private void Awake()
    {
            Instance = this;
        }


    #endregion
    
    public PlayerMovement movement { get; private set; }
    public PlayerSpriteManagement spriteManagement { get; private set; }
    private BoxCollider2D bc;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        spriteManagement = GetComponent<PlayerSpriteManagement>();
    }
}