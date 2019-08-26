using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform m_Player;
    private Camera m_Camera;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Player = PlayerManager.Instance.movement.transform;
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        m_Camera.transform.position = new Vector3(m_Player.position.x, m_Camera.transform.position.y, m_Camera.transform.position.z);
    }
}
