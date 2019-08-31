using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss1 : MonoBehaviour
{
    public GameObject barrier;
    public List<string> sentences = new List<string>();
    public GameObject SwordHud;
    public Sprite SwordHudSprite;

    private void OnDestroy()
    {
        if (!DialogHeardBefore.instance.GetBoolValue("string", false))
        {
            Dialog.instance.InitializeDialog(sentences);
            DialogHeardBefore.instance.GetBoolValue("sword", true);
            GameManager.instance.player.GetComponent<PlayerCombat>().Hud.transform.GetChild(1).GetComponent<Image>().sprite = SwordHudSprite;
            Destroy(barrier.gameObject);
        }
    }

}
