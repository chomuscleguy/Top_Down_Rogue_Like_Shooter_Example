using System.Collections.Generic;
using UnityEngine;

public abstract class BaseListUI<TData, TSlot> : BasePopup where TSlot : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] protected Transform content;
    [SerializeField] protected TSlot slotPrefab;

    protected readonly List<TSlot> slots = new();

    protected virtual void Clear()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] != null)
                Destroy(slots[i].gameObject);
        }

        slots.Clear();
    }

    protected TSlot CreateSlot()
    {
        var slot = Instantiate(slotPrefab, content);
        slots.Add(slot);
        return slot;
    }

    protected void Rebuild(IEnumerable<TData> dataList, System.Action<TSlot, TData> onBind)
    {
        Clear();

        foreach (var data in dataList)
        {
            var slot = CreateSlot();
            onBind?.Invoke(slot, data);
        }
    }
}