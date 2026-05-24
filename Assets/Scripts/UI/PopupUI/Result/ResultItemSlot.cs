using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultItemSlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI level;

    public void Init(ItemData d, int level)
    {
        icon.sprite = d.icon;
        this.level.text = level.ToString();
    }
}