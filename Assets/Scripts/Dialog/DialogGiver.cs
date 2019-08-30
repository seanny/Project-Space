using System.Collections.Generic;
using UnityEngine;

public class DialogGiver : MonoBehaviour
{
    public List<string> sentences = new List<string>();
    public string referencevariable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool istrue = DialogHeardBefore.instance.GetBoolValue(referencevariable);
        
        if (istrue)
        {
            if (sentences.Count > 0) Dialog.instance.InitializeDialog(sentences);
        }
    }
}

