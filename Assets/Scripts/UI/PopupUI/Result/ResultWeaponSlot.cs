using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultWeaponSlot : MonoBehaviour
{
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponLevel;
    [SerializeField] private TextMeshProUGUI activeTime;
    [SerializeField] private TextMeshProUGUI totalDamage;
    [SerializeField] private TextMeshProUGUI DamagePerSeconds;

    public void Init(WeaponRuntime runtime)
    {
        weaponIcon.sprite = runtime.Data.icon;
        weaponName.text = runtime.Data.displayName;
        weaponLevel.text = runtime.Level.ToString();

        float ownTime = Core.Instance.Game.Run.PlayTime - runtime.Stats.acquiredTime;
        activeTime.text = ownTime.ToString("F1");

        totalDamage.text = runtime.Stats.totalDamage.ToString("F0");

        float dps = ownTime > 0 ? runtime.Stats.totalDamage / ownTime : 0f;

        DamagePerSeconds.text = dps.ToString("F1");
    }

}
