using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    #region Singleton
    public static CameraManager instance;
    private void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    public void CameraShake()
    {

    }
}
