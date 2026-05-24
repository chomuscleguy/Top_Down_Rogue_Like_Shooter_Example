using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultUpgradeSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI level;

    public void Init(UpgradeData upgrade, int level)
    {
        icon.sprite = upgrade.icon;
        this.level.text = level.ToString();
    }
}
