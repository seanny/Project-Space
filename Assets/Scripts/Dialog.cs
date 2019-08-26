using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] lines;
    private int sentenceindex;
    public float TimeBetweenLetters;
    private bool inDialog;

    public GameObject TextBox;
    public GameObject ContinueButton;
    private Image im;

    private void Start()
    {
        im = TextBox.GetComponent<Image>();
        StartCoroutine(TypeSentences());
    }

    public IEnumerator TypeSentences()
    {
        textDisplay.text = "";
        im.enabled = true;
        ContinueButton.gameObject.SetActive(true);
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
        im.enabled = false;
        ContinueButton.gameObject.SetActive(false);
        textDisplay.text = "";
    }
}
