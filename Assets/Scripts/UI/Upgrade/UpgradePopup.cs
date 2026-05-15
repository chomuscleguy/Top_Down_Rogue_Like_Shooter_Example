using UnityEngine;
using UnityEngine.UI;

public class UpgradePopup : BaseListUI<UpgradeData, UpgradeSlot>
{
    [SerializeField] private UpgradeDatabase database;
    [SerializeField] private UpgradeDetailPanel detailPanel;
    [SerializeField] private Button returnButton;

    private UpgradeData currentData;

    private PlayerProgress progress => Core.Instance.Game.SaveData.progress;

    private int Gold
    {
        get => Core.Instance.Game.SaveData.gold;
        set => Core.Instance.Game.SaveData.gold = value;
    }

    private void Awake()
    {
        returnButton.onClick.AddListener(OnClickReturn);
    }

    private void Start()
    {
        Rebuild(database.upgrades, BindSlot);
    }

    private void BindSlot(UpgradeSlot slot, UpgradeData data)
    {
        int level = progress.GetLevel(data);

        slot.Init(data, level);
        slot.SetClick(OnSelectUpgrade);
    }

    private void OnClickUpgrade(UpgradeData data)
    {
        int level = progress.GetLevel(data);

        if (level >= data.MaxLevel)
            return;

        int cost = data.GetCost(level);

        if (Gold < cost)
            return;

        Core.Instance.Game.SpendGold(cost);
        progress.AddLevel(data);

        Core.Instance.Save.Save(Core.Instance.Game.SaveData);

        UpdateDetailPanel();
        UpdateSlots();
    }

    private void OnClickReturn()
    {
        int refund = CalculateTotalSpent();

        Core.Instance.Game.AddGold(refund);

        progress.ResetAll();

        Core.Instance.Save.Save(Core.Instance.Game.SaveData);

        UpdateSlots();
        UpdateDetailPanel();
    }

    private void OnSelectUpgrade(UpgradeData data)
    {
        currentData = data;
        ShowDetail(data);
    }

    private void UpdateDetailPanel()
    {
        if (currentData == null)
            return;

        ShowDetail(currentData);
    }

    private void UpdateSlots()
    {
        foreach (var slot in slots)
        {
            int level = progress.GetLevel(slot.Data);
            slot.UpdateLevelUI(level);
        }
    }

    private void ShowDetail(UpgradeData data)
    {
        int level = progress.GetLevel(data);
        detailPanel.Show(data, level, OnClickUpgrade);
    }

    private int CalculateTotalSpent()
    {
        int total = 0;

        foreach (var upgrade in database.upgrades)
        {
            int level = progress.GetLevel(upgrade);

            for (int i = 0; i < level; i++)
                total += upgrade.GetCost(i);
        }

        return total;
    }
}