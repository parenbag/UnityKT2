using System.Collections.Generic;
using UnityEngine;

public class BasicObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objPrefab;
    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> pool;

    private void Awake()
    {
        pool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetFromPool()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Debug.LogWarning("Pool is empty!");
            return null;
        }
    }

    public GameObject TryGetFromPool()
    {
        return GetFromPool();
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}