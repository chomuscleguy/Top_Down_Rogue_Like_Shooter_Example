using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPopup : BaseListUI<CharacterData, CharacterSlot>
{
    [SerializeField] private CharacterDatabase database;
    [SerializeField] private CharacterDetailPanel detailPanel;

    private void Start()
    {
        Refresh();
        InitDefaultSelection();
    }

    private void Refresh()
    {
        Rebuild(database.characters, BindSlot);
    }

    private void BindSlot(CharacterSlot slot, CharacterData data)
    {
        slot.Init(data);
        slot.SetClick(OnSelectCharacter);
    }

    private void OnSelectCharacter(CharacterData data)
    {
        Core.Instance.Game.SelectedCharacterID = data.id;

        detailPanel.Show(data);
    }

    private void InitDefaultSelection()
    {
        int id = Core.Instance.Game.SelectedCharacterID;

        var data = database.GetByID(id);

        if (data == null)
            data = database.characters[0];

        OnSelectCharacter(data);
    }
}