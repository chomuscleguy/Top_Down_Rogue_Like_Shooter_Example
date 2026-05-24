using UnityEngine;

public class CharacterSelectPopup : BasePopup
{
    [SerializeField] private BaseListUI<CharacterData, CharacterSlot> list;

    [SerializeField] private CharacterDatabase database;

    [SerializeField] private CharacterDetailPanel detailPanel;

    [SerializeField] private CharacterInfoPanel infoPanel;

    protected override void Init()
    {
        Refresh();
        InitDefaultSelection();
    }

    private void Refresh()
    {
        list.Rebuild(database.GetAll(), BindSlot);
    }

    private void BindSlot(CharacterSlot slot, CharacterData data)
    {
        slot.Init(data);
        slot.SetClick(OnSelectCharacter);
    }

    private void OnSelectCharacter(CharacterData data)
    {
        Core.Instance.Game.SelectedCharacterID = data.ID;

        detailPanel.Show(data);

        infoPanel.Refresh(data);
    }

    private void InitDefaultSelection()
    {
        int id = Core.Instance.Game.SelectedCharacterID;

        CharacterData data = database.Get(id);

        if (data == null)
        {
            var all = database.GetAll();

            if (all.Count <= 0)
                return;

            data = all[0];
        }

        OnSelectCharacter(data);
    }
}
