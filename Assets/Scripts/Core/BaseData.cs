using UnityEngine;

public abstract class BaseData : ScriptableObject
{
    [Header("Info")]
    [SerializeField] private int id;

    public int ID => id;

    public string displayName;

    [TextArea]
    public string description;

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
    }
#endif
}