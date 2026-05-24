using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapUISlot : MonoBehaviour, ISlot<MapThemeData>
{
    [SerializeField] private Image background;

    [SerializeField] private Image thumbnail;
    [SerializeField] private Button button;

    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private TextMeshProUGUI descText;

    private Action<MapThemeData> onClick;
    private MapThemeData data;

    private void Awake()
    {
        button.onClick.AddListener(() => onClick?.Invoke(data));
    }

    public void Init(MapThemeData d)
    {
        data = d;

        thumbnail.sprite = data.thumbnail;
        numberText.text = data.stageNumber;
        descText.text = data.description;
    }

    public void SetClick(Action<MapThemeData> callback)
    {
        onClick = callback;
    }

    public void SetSelected(bool selected)
    {
        Color color = background.color;

        color.a = selected ? 0.4f : 0f;

        background.color = color;
    }
}
