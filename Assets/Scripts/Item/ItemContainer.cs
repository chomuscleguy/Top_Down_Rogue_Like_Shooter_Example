using System.Collections.Generic;

public class ItemContainer
{
    private readonly Dictionary<ItemData, int> items = new();

    public void AddItem(ItemData item)
    {
        if (item == null)
            return;

        if (items.ContainsKey(item))
            items[item]++;
        else
            items[item] = 1;
    }

    public int GetLevel(ItemData item)
    {
        if (items.TryGetValue(item, out int level))
            return level;

        return 0;
    }

    public IEnumerable<(ItemData item, int level)>
        GetAllItems()
    {
        foreach (var pair in items)
        {
            yield return (pair.Key, pair.Value);
        }
    }
}