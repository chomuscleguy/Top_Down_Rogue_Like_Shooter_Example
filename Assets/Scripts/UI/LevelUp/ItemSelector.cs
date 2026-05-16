using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ItemSelector
{
    public static List<ItemData> GetRandomSelectableItems(
        RunData run,
        ItemDatabase database,
        int count)
    {
        List<ItemData> pool = new();

        foreach (var item in database.items)
        {
            if (IsSelectable(run, item))
                pool.Add(item);
        }

        if (pool.Count == 0)
            return pool;

        Shuffle(pool);

        int takeCount = Mathf.Min(count, pool.Count);

        return pool.Take(takeCount).ToList();
    }

    private static bool IsSelectable(RunData run, ItemData item)
    {
        int level = run.Items.GetLevel(item);

        if (item is WeaponData weapon)
            return level < weapon.MaxLevel;

        if (item is BuffData buff)
            return level < buff.MaxLevel;

        return false;
    }

    private static void Shuffle(List<ItemData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
}