using System.Collections.Generic;

public static class UpgradeCalculator
{
    public static CharacterStats Calculate(List<UpgradeData> upgrades, PlayerProgress progress)
    {
        CharacterStats result = default;

        foreach (var upgrade in upgrades)
        {
            int level = progress.GetLevel(upgrade);

            for (int i = 0; i < level; i++)
            {
                result += upgrade.levels[i].stats;
            }
        }

        return result;
    }
}