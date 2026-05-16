using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDetailPanel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text gold;
    [SerializeField] private Button button;

    private UpgradeData currentData;
    private Action<UpgradeData> onUpgradeClick;

    private void Awake()
    {
        button.onClick.AddListener(HandleUpgradeClick);
    }

    public void Show(UpgradeData data, int level, Action<UpgradeData> callback)
    {
        currentData = data;
        onUpgradeClick = callback;

        icon.sprite = data.icon;
        nameText.text = data.displayName;

        descriptionText.text = UpgradeDescriptionFactory.Create(data, level);

        if (level >= data.MaxLevel)
        {
            gold.text = "MAX";
            button.interactable = false;
        }
        else
        {
            var levelData = data.GetLevel(level + 1);

            gold.text = levelData.cost.ToString();
            button.interactable = true;
        }
    }

    private void HandleUpgradeClick()
    {
        onUpgradeClick?.Invoke(currentData);
    }
}