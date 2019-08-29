using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>();
    }
    CinemachineVirtualCamera cam;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.player.transform != null) cam.Follow = GameManager.instance.player.transform;
        }
        
    }
}
