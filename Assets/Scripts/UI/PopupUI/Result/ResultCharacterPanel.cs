using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultCharacterPanel: MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI characterName;

    public void Init(CharacterData data)
    {
        icon.sprite = data.icon;
        characterName.text = data.characterName;
    }
}
