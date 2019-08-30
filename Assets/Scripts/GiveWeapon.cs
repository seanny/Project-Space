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
    public Sprite tablenogun;
    public List<string> Sentences;

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

        List<Transform> HudObjects = new List<Transform>();

        for (int i = 0; i < Hud.transform.childCount; i++)
        {
            Hud.transform.GetChild(i);

        }
        foreach (Transform img in HudObjects)
        {
            img.GetComponent<Image>().enabled = true;
        }
        EKey.SetActive(false);
        Destroy(barrier.gameObject);
        table.GetComponent<SpriteRenderer>().sprite = tablenogun;
        DialogHeardBefore.instance.intro = true;
        Dialog.instance.InitializeDialog(Sentences);
        transform.parent = GameObject.FindGameObjectWithTag("Door").transform;
        transform.parent = null;
        Destroy(this);


    }
}
