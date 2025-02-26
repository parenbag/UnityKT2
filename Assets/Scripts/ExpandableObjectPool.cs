using System.Collections.Generic;
using UnityEngine;

public class ExpandableObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objPrefab;
    [SerializeField] private int startPoolSize = 5;
    [SerializeField] private int maxPoolSize = 20;

    private Queue<GameObject> availableObjects;
    private List<GameObject> allObjects;

    private void Awake()
    {
        availableObjects = new Queue<GameObject>();
        allObjects = new List<GameObject>();

        for (int i = 0; i < startPoolSize; i++)
        {
            CreateNewObject();
        }
    }

    private void CreateNewObject()
    {
        if (allObjects.Count < maxPoolSize)
        {
            GameObject obj = Instantiate(objPrefab);
            obj.SetActive(false);
            availableObjects.Enqueue(obj);
            allObjects.Add(obj);
        }
    }

    public GameObject TryGetFromPool()
    {
        if (availableObjects.Count > 0)
        {
            GameObject obj = availableObjects.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else if (allObjects.Count < maxPoolSize)
        {
            CreateNewObject();
            return TryGetFromPool();
        }
        else
        {
            Debug.LogWarning("Max pool size reached!");
            return null;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        if (allObjects.Contains(obj))
        {
            obj.SetActive(false);
            availableObjects.Enqueue(obj);
        }
        else
        {
            Debug.LogWarning("Trying to return an object that wasn't created by this pool!");
        }
    }
}