using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiveWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject barrier;
    public Transform table;
    public float PickupDistance;
    public GameObject EKey;
    private GameObject Hud;
    public Sprite HudSwordSprite;

    void Start()
    {
        if (DialogHeardBefore.instance.intro != true)
        {

        } else
        {
            Destroy(barrier.gameObject);
            Destroy(this);
        }

       if (GameManager.instance != null) transform.parent = GameManager.instance.transform;
        Hud = GameObject.FindGameObjectWithTag("HUD");

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Vector2.Distance(GameManager.instance.player.transform.position, table.transform.position) < PickupDistance)
        {
            Debug.Log("Distance is short enough");
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
        //Hud.SetActive(true);

        Debug.Log(Hud.transform.childCount);
        List<Transform> HudObjects = new List<Transform>
        {
        };
        foreach (Transform img in HudObjects)
        {
            img.GetComponent<Image>().enabled = true;
        }

        Destroy(this);

        DialogHeardBefore.instance.intro = true;

    }
}
