using System.Collections.Generic;
using UnityEngine;

public class DialogGiver : MonoBehaviour
{
    public List<string> sentences = new List<string>();
    public string referencevariable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Colliding");

        bool istrue = DialogHeardBefore.instance.GetBoolValue(referencevariable, false);
        //Debug.Log(istrue.ToString());
        if (!istrue)
        {
           // Debug.Log("It is true");
            
            if (sentences.Count > 0) Dialog.instance.InitializeDialog(sentences);
            DialogHeardBefore.instance.GetBoolValue(referencevariable, true);
        }
    }
}

