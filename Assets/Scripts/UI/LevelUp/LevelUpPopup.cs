using System;
using System.Collections.Generic;

public class LevelUpPopup : BaseListUI<ItemData, LevelUpSlot>
{
    private Func<ItemData, int> getItemLevel;
    private Action<ItemData> onSelect;

    public void Init(List<ItemData> items, Func<ItemData, int> getItemLevel, Action<ItemData> onSelect)
    {
        this.getItemLevel = getItemLevel;
        this.onSelect = onSelect;

        Rebuild(items, BindSlot);
    }

    private void BindSlot(LevelUpSlot slot, ItemData data)
    {
        int level = getItemLevel(data);

        var viewData = new LevelUpSlotViewData
        {
            icon = data.icon,
            name = data.itemName,
            description = LevelUpDescriptionFactory.Create(data, level),
            data = data
        };

        slot.Init(viewData, OnSelect);
    }

    private void OnSelect(ItemData data)
    {
        onSelect?.Invoke(data);

        Core.Instance.UI.CloseTopPopup();
    }
}