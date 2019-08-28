using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject barrier;
    public Transform table;
    public float PickupDistance;
    public GameObject EKey;
    public GameObject Hud;
    public GameObject HudGunSprite;

    void Start()
    {
        if (DialogHeardBefore.instance.intro != true)
        {

        } else
        {
            Destroy(barrier.gameObject);
            Destroy(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(table.position.x - GameManager.instance.player.transform.position.x) < PickupDistance)
        {
            EKey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                GiveGun();
            }
        } else
        {
            EKey.SetActive(false);
        }
    }

    void GiveGun()
    {
        Hud.SetActive(true);

        Destroy(this);

    }
}
