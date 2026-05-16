using UnityEngine;

public class MapSelectPopup : BaseListUI<MapThemeData, MapUISlot>
{
    [SerializeField] private MapThemeDatabase database;
    private MapUISlot selectedSlot;

    private void Start()
    {
        Refresh();
        InitDefaultSelection();
    }

    private void Refresh()
    {
        Rebuild(database.map, BindSlot);
    }

    private void BindSlot(MapUISlot slot, MapThemeData data)
    {
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
        Core.Instance.Game.SelectedMapID = data.id;
    }

    private void InitDefaultSelection()
    {
        int id = Core.Instance.Game.SelectedMapID;

        var data = database.GetByID(id);

        if (data == null)
            data = database.map[0];

        OnSelectMap(data);
    }
}
