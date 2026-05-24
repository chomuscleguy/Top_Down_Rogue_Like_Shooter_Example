using System;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectPopup : BasePopup
{
    [SerializeField] private BaseListUI<MapThemeData, MapUISlot> list;
    [SerializeField] private MapThemeDatabase database;

    private readonly Dictionary<MapThemeData, MapUISlot> slotTable = new();

    private MapUISlot selectedSlot;

    protected override void Init()
    {
        Refresh();
        InitDefaultSelection();
    }

    private void Refresh()
    {
        list.Rebuild(database.GetAll(), BindSlot);
    }

    private void BindSlot(MapUISlot slot, MapThemeData data)
    {
        slotTable[data] = slot;

        slot.Init(data);

        slot.SetSelected(false);

        slot.SetClick((selectedData) => OnClickSlot(slot, selectedData));
    }

    private void OnClickSlot(MapUISlot slot, MapThemeData data)
    {
        SelectSlot(slot);

        OnSelectMap(data);
    }

    private void SelectSlot(MapUISlot slot)
    {
        if (selectedSlot != null)
            selectedSlot.SetSelected(false);

        selectedSlot = slot;

        selectedSlot.SetSelected(true);
    }

    private void OnSelectMap(MapThemeData data)
    {
        Core.Instance.Game.SelectedMapID = data.ID;
    }

    private void InitDefaultSelection()
    {
        int id = Core.Instance.Game.SelectedMapID;

        var data = database.Get(id);

        if (data == null)
        {
            var all = database.GetAll();

            if (all.Count <= 0)
                return;

            data = all[0];
        }

        OnSelectMap(data);

        if (slotTable.TryGetValue(data, out var slot))
        {
            SelectSlot(slot);
        }
    }
}