using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject MainMenuParent;

    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        } 

        player = FindObjectOfType<PlayerMovement>().gameObject;


    }


    [HideInInspector] public GameObject player;

    public void StartGame()
    {
        StartCoroutine(StartingGame());
    }

    IEnumerator StartingGame()
    {
        Fader.instance.FadeOut();
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        MainMenuParent.SetActive(false);
        Fader.instance.FadeIn();
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
