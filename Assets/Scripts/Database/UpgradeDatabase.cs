using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Database/Upgrade")]

public class UpgradeDatabase : ScriptableObject
{
    public List<UpgradeData> upgrades;
}