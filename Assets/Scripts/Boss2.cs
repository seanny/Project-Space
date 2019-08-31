using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    IEnumerator EndGame()
    {
        Fader.instance.FadeOut();
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(7);

    }



}
