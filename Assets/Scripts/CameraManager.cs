using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
   /* #region Singleton
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
    #endregion*/

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void CameraShake()
    {

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.instance.player != null)
        {
            CinemachineVirtualCamera vcam = FindObjectOfType<CinemachineVirtualCamera>();

            //var vcam = GetComponent<CinemachineVirtualCamera>();

            vcam.LookAt = GameManager.instance.player.transform.GetChild(0).transform;
            vcam.Follow = GameManager.instance.player.transform.GetChild(0).transform;


            Debug.Log("Camera");
        }
    }
}
