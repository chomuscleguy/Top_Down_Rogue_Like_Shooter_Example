using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPopup : BasePopup
{
    [SerializeField] private BaseListUI<ItemData, LevelUpSlot> list;

    protected override void Init()
    {
        var run = Core.Instance.Game.Run;

        var items = ItemSelector.GetRandomSelectableItems(run, Core.Instance.Data.Items, 3);

        list.Rebuild(items, BindSlot);
    }

    private void BindSlot(LevelUpSlot slot, ItemData data)
    {
        var run = Core.Instance.Game.Run;

        int level = run.Items.GetLevel(data);

        var viewData = new LevelUpSlotViewData
        {
            icon = data.icon,
            name = data.displayName,
            description = LevelUpDescriptionFactory.Create(data, level),
            data = data
        };

        slot.Init(viewData, OnSelect);
    }

    private void OnSelect(ItemData data)
    {
        Core.Instance.Game.Run.AddItem(data);

        Core.Instance.UI.CloseTopPopup();
    }
}