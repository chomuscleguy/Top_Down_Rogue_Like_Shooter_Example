using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDatabase<T> : ScriptableObject, IBaseDatabase where T : BaseData
{
    [SerializeField] protected List<T> datas = new();

    protected Dictionary<int, T> map;

    public virtual void Init()
    {
        map = new Dictionary<int, T>();

        foreach (T data in datas)
        {
            if (data == null)
                continue;

            if (map.ContainsKey(data.ID))
            {
                Debug.LogError($"중복 ID 발견: {data.ID} ({data.name})", data);

                continue;
            }

            map.Add(data.ID, data);
        }
    }

    public T Get(int id)
    {
        if (map.TryGetValue(id, out T data))
            return data;

        return null;
    }

    public IReadOnlyList<T> GetAll()
    {
        return datas;
    }
}