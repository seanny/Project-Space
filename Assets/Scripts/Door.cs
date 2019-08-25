using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public int SceneToUnloadBuildIndex;
    public int SceneToLoadBuildIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(SceneTransitions());            
        }

    }

    IEnumerator SceneTransitions()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(SceneToLoadBuildIndex, LoadSceneMode.Additive);
        while (!load.isDone) { yield return null; }
        AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneToUnloadBuildIndex, UnloadSceneOptions.None);
        while (!unload.isDone) { yield return null; }

    }

    

}
