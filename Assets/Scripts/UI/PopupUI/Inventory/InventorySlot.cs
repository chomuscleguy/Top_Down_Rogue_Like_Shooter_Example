using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private Sprite lockedSprite;

    public void Set(ItemData item, int level)
    {
        if (item == null)
        {
            Clear();
            return;
        }

        icon.sprite = item.icon;

        levelText.text = level > 1 ? $"Lv.{level}" : "";
    }

    public void Clear()
    {
        icon.sprite = lockedSprite;

        levelText.text = "";
    }
}