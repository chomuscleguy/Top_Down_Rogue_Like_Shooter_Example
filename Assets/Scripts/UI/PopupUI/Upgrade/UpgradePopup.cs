using UnityEngine;
using UnityEngine.UI;

public class UpgradePopup : BasePopup
{
    [SerializeField] private BaseListUI<UpgradeData, UpgradeSlot> list;
    [SerializeField] private UpgradeDatabase database;
    [SerializeField] private UpgradeDetailPanel detailPanel;
    [SerializeField] private Button returnButton;

    private UpgradeData currentData;

    private PlayerProgress progress => Core.Instance.Game.Progress;

    protected override void Init()
    {
        database = Core.Instance.Data.Upgrades;
        returnButton.onClick.AddListener(OnClickReturn);
        list.Rebuild(database.GetAll(), BindSlot);
    }

    private void BindSlot(UpgradeSlot slot, UpgradeData data)
    {
        int level = progress.GetLevel(data);

        slot.Init(data, level);
        slot.SetClick(OnSelectUpgrade);
    }

    private void OnSelectUpgrade(UpgradeData data)
    {
        currentData = data;
        ShowDetail(data);
    }

    private void OnClickUpgrade(UpgradeData data)
    {
        int level = progress.GetLevel(data);

        if (level >= data.MaxLevel)
            return;

        int cost = data.GetCost(level);

        if (!progress.SpendGold(cost))
            return;

        progress.AddLevel(data);

        Core.Instance.Save.Save(Core.Instance.Game.SaveData);

        UpdateDetailPanel();
        UpdateSlots();
    }

    private void OnClickReturn()
    {
        int refund = CalculateTotalSpent();

        progress.AddGold(refund);
        progress.ResetAll();

        Core.Instance.Save.Save(Core.Instance.Game.SaveData);

        UpdateSlots();
        UpdateDetailPanel();
    }

    private void UpdateDetailPanel()
    {
        if (currentData == null)
            return;

        ShowDetail(currentData);
    }

    private void UpdateSlots()
    {
        list.Rebuild(database.GetAll(), BindSlot);
    }

    private void ShowDetail(UpgradeData data)
    {
        int level = progress.GetLevel(data);
        detailPanel.Show(data, level, OnClickUpgrade);
    }

    private int CalculateTotalSpent()
    {
        return progress.CalculateTotalSpent(database.GetAll());
    }
}
