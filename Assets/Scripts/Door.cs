using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    public int SceneToUnloadBuildIndex;
    public int SceneToLoadBuildIndex;

    public bool fadetoscene;

    private void Start()
    {
        CurrentLevel = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            StartCoroutine(SceneTransitions());            
        }

    }

    public string PreviousLevel;
    private string CurrentLevel;

    IEnumerator SceneTransitions()
    {
        if (fadetoscene)
        {
            transform.parent = GameManager.instance.transform;
            GetComponent<BoxCollider2D>().enabled = false;
            Time.timeScale = 0f;
            Fader.instance.FadeOut();
            yield return new WaitForSecondsRealtime(1f);

            if (SceneToLoadBuildIndex != 999)
            {
                AsyncOperation load = SceneManager.LoadSceneAsync(SceneToLoadBuildIndex, LoadSceneMode.Additive);
                while (!load.isDone) { yield return new WaitForEndOfFrame(); }
            }

            if (SceneToUnloadBuildIndex != 999)
            {
                AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneToUnloadBuildIndex, UnloadSceneOptions.None);
                while (!unload.isDone) { yield return new WaitForEndOfFrame(); }
            }


            /*object[] obj = FindObjectsOfType(typeof(GameObject));
            foreach (object o in obj)
            {
                GameObject g = (GameObject)o;

               
                    g?.GetComponent<Door>();

                    if (g.GetComponent<Door>().PreviousLevel.Equals(CurrentLevel))
                    {
                        GameManager.instance.player.transform.position = g.transform.position;
                    }
                
                
            }
            */
            Time.timeScale = 1f;
            Fader.instance.FadeIn();
            yield return new WaitForSecondsRealtime(1f);
        } else
        {
            if (!GameManager.instance.ActiveScenes.Contains(SceneToLoadBuildIndex))
            {
                if (SceneToLoadBuildIndex != 999)
                {
                    AsyncOperation load = SceneManager.LoadSceneAsync(SceneToLoadBuildIndex, LoadSceneMode.Additive);
                    while (!load.isDone) { yield return null; }
                }

                if (SceneToUnloadBuildIndex != 999)
                {
                    AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneToUnloadBuildIndex, UnloadSceneOptions.None);
                    while (!unload.isDone) { yield return null; }
                }
            }
        }

        //Destroy(gameObject);

    }

    

}
