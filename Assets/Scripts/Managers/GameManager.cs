using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private bool startinggame;

    [HideInInspector] public GameObject player;

    public void StartGame()
    {
        if (startinggame != true)
        {
            StartCoroutine(StartingGame());
        }
    }

    IEnumerator StartingGame()
    {
        startinggame = true;
        Fader.instance.FadeOut();
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        MainMenuParent.SetActive(false);
        AsyncOperation load = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        while (!load.isDone)
        {
            yield return null;
        }
        Fader.instance.FadeIn();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        startinggame = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
