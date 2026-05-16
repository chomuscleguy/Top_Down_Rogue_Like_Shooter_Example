using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class UpgradeSlot : MonoBehaviour
{
    
    [Header("UI")]
    [SerializeField] private Image icon;
    [SerializeField] private Button button;

    [Header("Level UI")]
    [SerializeField] private Transform levelRoot;
    [SerializeField] private Image levelPrefab;
    [SerializeField] private Color activeColor = Color.yellow;
    [SerializeField] private Color inactiveColor = Color.gray;

    private List<Image> levelImages = new();
    private Action<UpgradeData> onClick;

    private UpgradeData data;
    public UpgradeData Data => data;

    private void Awake()
    {
        button.onClick.AddListener(() => onClick?.Invoke(data));
    }

    public void Init(UpgradeData d, int level)
    {
        data = d;

        icon.sprite = data.icon;

        BuildLevelUI(data.MaxLevel);
        UpdateLevelUI(level);
    }

    public void SetClick(Action<UpgradeData> callback)
    {
        onClick = callback;
    }

    private void BuildLevelUI(int maxLevel)
    {
        foreach (var img in levelImages)
            Destroy(img.gameObject);

        levelImages.Clear();

        for (int i = 0; i < maxLevel; i++)
        {
            var img = Instantiate(levelPrefab, levelRoot);
            img.color = inactiveColor;
            levelImages.Add(img);
        }
    }

    public void UpdateLevelUI(int level)
    {
        for (int i = 0; i < levelImages.Count; i++)
        {
            levelImages[i].color = (i < level) ? activeColor : inactiveColor;
        }
    }
}