using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour, ISlot<CharacterData>
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button button;

    [SerializeField] private CharacterData data;
    private Action<CharacterData> onClick;

    private void Awake()
    {
        button.onClick.AddListener(() => onClick?.Invoke(data));
    }

    public void Init(CharacterData d)
    {
        data = d;

        icon.sprite = data.icon;
        nameText.text = data.characterName;
    }

    public void SetClick(Action<CharacterData> callback)
    {
        onClick = callback;
    }
}