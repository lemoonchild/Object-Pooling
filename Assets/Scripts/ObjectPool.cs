using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Pool Settings")]
    public GameObject prefab;
    public int poolSize = 10;

    private List<GameObject> pool = new List<GameObject>();

    void Awake()
    {
        // Pre-instancia todos los objetos al inicio
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    // Devuelve un objeto disponible del pool
    public GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        Debug.LogWarning("Pool expandido: considera aumentar el poolSize");
        GameObject newObj = Instantiate(prefab);
        pool.Add(newObj);
        return newObj;
    }

    // Devuelve un objeto al pool 
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}