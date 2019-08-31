using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public List<string> sentences = new List<string>();

    private void OnDestroy()
    {
        if (!DialogHeardBefore.instance.GetBoolValue("morpher", false))
        {
            Dialog.instance.InitializeDialog(sentences);
            DialogHeardBefore.instance.GetBoolValue("morpher", true);
        }
    }



}
