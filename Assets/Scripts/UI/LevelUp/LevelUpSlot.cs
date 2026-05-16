using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSlot : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image icon;
    [SerializeField] private Button button;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;

    private Action<ItemData> onClick;
    private ItemData data;

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            onClick?.Invoke(data);
        });
    }

    public void Init(LevelUpSlotViewData viewData, Action<ItemData> onClick)
    {
        data = viewData.data;
        this.onClick = onClick;

        icon.sprite = viewData.icon;
        nameText.text = viewData.name;
        descText.text = viewData.description;
    }
}