using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public static Pooler instance;

    private void Awake()
    {
        instance = this;
    }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }


    public Dictionary<string, Queue<GameObject>> DictionaryPool;
    public List<Pool> pools;

    private void Start()
    {
        DictionaryPool = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = (GameObject)Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            DictionaryPool.Add(pool.tag, objectPool);
        }

    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, int damage)
    {
        if (!DictionaryPool.ContainsKey(tag)) { Debug.LogWarning("Pool with tag " + tag + " does not exist"); return null; }

        GameObject objectToSpawn = DictionaryPool[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectPooled();
        }

        DictionaryPool[tag].Enqueue(objectToSpawn);
        return objectToSpawn;

    }

}
