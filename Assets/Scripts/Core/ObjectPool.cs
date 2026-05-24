using UnityEngine;
using System.Collections.Generic;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T prefab;
    private int size;
    private Transform parent;
    private Queue<T> pool = new Queue<T>();

    public void Init(T _prefab, int _size, Transform _parent)
    {
        prefab = _prefab;
        size = _size;
        parent = _parent;

        ExpandPool();
    }
    private void ExpandPool()
    {
        for (int i = 0; i < size; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public T Get()
    {
        if (pool.Count == 0)
        {
            ExpandPool();
        }

        T obj = pool.Dequeue();

        obj.transform.SetParent(null);
        obj.gameObject.SetActive(true);

        return obj;
    }

    public void Return(T obj)
    {
        obj.transform.SetParent(parent);
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
