using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    public int SceneToUnloadBuildIndex;
    public int SceneToLoadBuildIndex;

    public bool fadetoscene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(SceneTransitions());            
        }

    }

    IEnumerator SceneTransitions()
    {
        if (fadetoscene)
        {
            transform.parent = GameManager.instance.transform;
            GetComponent<BoxCollider2D>().enabled = false;
            Time.timeScale = 0f;
            Fader.instance.FadeOut();
            yield return new WaitForSecondsRealtime(1f);

            AsyncOperation load = SceneManager.LoadSceneAsync(SceneToLoadBuildIndex, LoadSceneMode.Additive);
            while (!load.isDone) { yield return new WaitForEndOfFrame(); }
            AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneToUnloadBuildIndex, UnloadSceneOptions.None);
            while (!unload.isDone) { yield return new WaitForEndOfFrame(); }
            Debug.Log("Test");

            Time.timeScale = 1f;
            Fader.instance.FadeIn();
            yield return new WaitForSecondsRealtime(1f);
        } else
        {

            AsyncOperation load = SceneManager.LoadSceneAsync(SceneToLoadBuildIndex, LoadSceneMode.Additive);
            while (!load.isDone) { yield return null; }
            AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneToUnloadBuildIndex, UnloadSceneOptions.None);
            while (!unload.isDone) { yield return null; }
        }

        Destroy(gameObject);

    }

    

}
