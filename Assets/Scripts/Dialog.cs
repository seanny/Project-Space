using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] lines;
    private int sentenceindex;
    public float TimeBetweenLetters;
    private bool inDialog;


    private void Start()
    {
        StartCoroutine(TypeSentences());
    }

    public IEnumerator TypeSentences()
    {
        textDisplay.text = "";
        inDialog = true;
        foreach (char letters in lines[sentenceindex].ToCharArray())
        {
            textDisplay.text += letters;
            yield return new WaitForSeconds(TimeBetweenLetters);
        }

        inDialog = false;
    }

    public void ContinueDialog()
    {
        if (!inDialog)
        {
            if (sentenceindex < lines.Length - 1)
            {
                sentenceindex++;
                StartCoroutine(TypeSentences());
            } else if (sentenceindex == lines.Length - 1)
            {
                EndDialog();
            }
        } else
        {
            StopAllCoroutines();
            textDisplay.text = lines[sentenceindex];
            inDialog = false;
        }
    }

    void EndDialog()
    {
        Debug.Log("Ending Dialog");
    }
}
