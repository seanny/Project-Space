using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject MainMenuParent;

    public static GameManager instance;

    public Vector2 spawnPos;

    public List<int> ActiveScenes = new List<int>();

    private void Awake()
    {
        
            instance = this;
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        
    }

    public List<string> startdialog;

    private bool startinggame;

    [HideInInspector] public GameObject player;

    public void StartGame()
    {
        if (startinggame != true)
        {
            StartCoroutine(StartingGame());
        }
    }

    /*private void Update()
    {
        //RenderSettings.skybox.SetFloat("_Rotation", Time.time * 2f);
    }*/

    IEnumerator StartingGame()
    {
        AudioManager.instance.PlaySound("Music");
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
        Dialog.instance.InitializeDialog(startdialog);
        Fader.instance.FadeIn();
        Time.timeScale = 1f;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        yield return new WaitForSecondsRealtime(1f);
        startinggame = false;
    }

    public void TeleportToCheckPoint()
    {
        player.transform.position = spawnPos;

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
