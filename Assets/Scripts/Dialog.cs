using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public List<string> lines;
    private int sentenceindex;
    public float TimeBetweenLetters;
    private bool inDialog;

    public GameObject TextBox;
    public GameObject ContinueButton;
    private Image im;
    public GameObject DialogSystem;

    public static Dialog instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        im = TextBox.GetComponent<Image>();
    }

    
    public void ContinueDialog()
    {
        if (!inDialog)
        {
            if (sentenceindex < lines.Count - 1)
            {
                sentenceindex++;
                StartCoroutine(TypeSentences());
            } else if (sentenceindex == lines.Count - 1)
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
        im.enabled = false;
        ContinueButton.gameObject.SetActive(false);
        textDisplay.text = "";
        Time.timeScale = 1f;
    }
    public IEnumerator TypeSentences()
    {

        textDisplay.text = "";
        yield return null;
        Time.timeScale = 0f;
        DialogSystem.SetActive(true);
        im.enabled = true;
        ContinueButton.gameObject.SetActive(true);
        inDialog = true;
        foreach (char letters in lines[sentenceindex].ToCharArray())
        {
            textDisplay.text += letters;
            yield return new WaitForSecondsRealtime(TimeBetweenLetters);
        }

        inDialog = false;
    }

    public void InitializeDialog(List<string> sentences)
    {
        lines.Clear();
        for(int i = 0; i < sentences.Count; i++)
        {
            lines.Add(sentences[i]);
        }
        sentenceindex = 0;

        StartCoroutine(TypeSentences());
    }
}
