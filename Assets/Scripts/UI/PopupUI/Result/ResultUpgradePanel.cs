using UnityEngine;

public class ResultUpgradePanel : BaseListUI<(UpgradeData upgrade, int level), ResultUpgradeSlot>
{
    public void Init(PlayerProgress progress)
    {
        Rebuild(progress.GetAllUpgrades(), Bind);
    }

    private void Bind(ResultUpgradeSlot slot, (UpgradeData upgrade, int level) data)
    {
        slot.Init(data.upgrade, data.level);
    }
}