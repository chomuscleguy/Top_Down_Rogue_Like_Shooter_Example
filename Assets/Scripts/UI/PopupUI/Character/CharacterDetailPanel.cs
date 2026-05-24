using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDetailPanel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;

    [SerializeField] private Image weaponIcon;

    public void Show(CharacterData data)
    {
        icon.sprite = data.icon;
        nameText.text = data.characterName;
        descriptionText.text = data.description;
        weaponIcon.sprite = data.baseWeapon.icon;
    }
}