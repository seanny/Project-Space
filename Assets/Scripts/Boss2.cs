using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss2 : MonoBehaviour
{
    public List<string> sentences = new List<string>();

    private void OnDestroy()
    {
        //Dialog.instance.InitializeDialog(sentences);
        SceneManager.LoadScene(7, LoadSceneMode.Single);
     
    }



}
