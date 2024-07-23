using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
{
    // Tên và hàng đợi của Component tương ứng
    public static Dictionary<string, Queue<Component>> poolDict = new Dictionary<string, Queue<Component>>();

    // Tên và Component tương ứng
    public static Dictionary<string, Component> poolLookUp = new Dictionary<string, Component>();

    public static void EnqueueObject<T>(string itemName, T item) where T : Component
    {
        if (!item.gameObject.activeSelf) 
            return;

        item.transform.position = Vector3.zero;
        item.gameObject.SetActive(false);
        poolDict[itemName].Enqueue((T)item);
    }

    public static T DequeueObject<T>(string itemName) where T : Component
    {
        if (poolDict[itemName].TryDequeue(out var item))
        {
            item.gameObject.SetActive(true);
            return (T)item;
        }
        T newInstance = Object.Instantiate((T)poolLookUp[itemName]);
        EnqueueObject<T>(itemName, newInstance);
        return (T)DequeueObject<T>(itemName);
    }

    public static void SetUpPool<T>(T itemPrefab, int poolSize, string itemName) where T : Component
    {
        poolLookUp.Add(itemName, itemPrefab);

        poolDict.Add(itemName, new Queue<Component>());

        for (int i = 0; i < poolSize; i++)
        {
            T instance = Object.Instantiate(itemPrefab);
            EnqueueObject<T>(itemName, instance);
        }
    }
}
