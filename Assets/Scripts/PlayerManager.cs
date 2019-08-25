using System;
using UnityEngine;

/// <summary>
/// Manager class which will be used for accessing player scripts.
/// </summary>
public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager Instance { get; private set; }
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    #endregion
    
    public PlayerMovement movement { get; private set; }
    public PlayerSpriteManagement spriteManagement { get; private set; }

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        spriteManagement = GetComponent<PlayerSpriteManagement>();
    }
}