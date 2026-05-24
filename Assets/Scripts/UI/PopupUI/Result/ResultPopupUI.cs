using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPopupUI : BasePopup
{
    [SerializeField] private TextMeshProUGUI stageNum;
    [SerializeField] private ResultCharacterPanel character;
    [SerializeField] private ResultPlayerInfoPanel info;
    [SerializeField] private ResultItemPanel item;
    [SerializeField] private ResultUpgradePanel upgrade;
    [SerializeField] private ResultWeaponPanel weapon;
    [SerializeField] private Button mainMenuButton;

    protected override void Init()
    {
        Core c = Core.Instance;

        MapThemeData map = c.Data.Maps.Get(c.Game.SelectedMapID);
        CharacterData characterData = c.Data.Characters.Get(c.Game.SelectedCharacterID);
        ItemContainer items = c.Game.Run.Items;
        PlayerProgress progress = c.Game.Progress;

        Player player = FindFirstObjectByType<Player>();

        var runtimes = player.WeaponSystem.Runtimes;

        stageNum.text = $"{map.stageNumber} - {map.displayName}";

        this.character.Init(characterData);
        this.info.Init();
        this.item.Init(items);
        this.upgrade.Init(progress);
        this.weapon.Init(runtimes);

        mainMenuButton.onClick.AddListener(OnClickMainMenu);
    }

    public void OnClickMainMenu()
    {
        Core c = Core.Instance;

        RunData run = c.Game.Run;
        SaveData save = c.Game.SaveData;

        save.progress.AddGold(c.Game.Run.Gold);

        c.Save.Save(save);

        c.Game.ChangeState(GameState.Lobby);
    }
}
