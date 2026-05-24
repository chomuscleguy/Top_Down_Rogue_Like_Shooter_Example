using System.Linq;
using UnityEngine;

public class ResultItemPanel : BaseListUI<(ItemData item, int level), ResultItemSlot>
{
    public void Init(ItemContainer items)
    {
        var sorted = items.GetAllItems().OrderBy(x => x.item is not WeaponData);

        Rebuild(sorted, Bind);
    }

    private void Bind(ResultItemSlot slot, (ItemData item, int level) data)
    {
        slot.Init(data.item, data.level);
    }
}