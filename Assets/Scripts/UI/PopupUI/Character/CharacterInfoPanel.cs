using System;
using System.Text;
using TMPro;
using UnityEngine;

public class CharacterInfoPanel : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] private TextMeshProUGUI combatText;
    [Header("Projectile")]
    [SerializeField] private TextMeshProUGUI projectile;

    [Header("Survival")]
    [SerializeField] private TextMeshProUGUI survivalText;

    [Header("Movement")]
    [SerializeField] private TextMeshProUGUI movementText;

    [Header("Utility")]
    [SerializeField] private TextMeshProUGUI utilityText;

    public void Refresh(CharacterData data)
    {
        combatText.text = BuildCombatText(data.stats.combat);
        projectile.text = BuildProjectileText(data.stats.projectile);
        survivalText.text = BuildSurvivalText(data.stats.survival);
        movementText.text = BuildMovementText(data.stats.movement);
        utilityText.text = BuildUtilityText(data.stats.utility);
    }

    

    private string BuildCombatText(CombatStats s)
    {
        StringBuilder sb = new();

        sb.AppendLine($"Damage : {s.damage:P0}");
        sb.AppendLine($"Crit Chance : {s.critChance:P0}");
        sb.AppendLine($"Crit Damage : {s.critDamageMultiplier:P0}");
        sb.AppendLine($"CooldownReduction : {s.cooldownMultiplier:P0}");

        return sb.ToString();
    }

    private string BuildProjectileText(ProjectileStats s)
    {
        StringBuilder sb = new();

        sb.AppendLine($"Projectile Count : {s.projectileCount:P0}");

        return sb.ToString();
    }

    private string BuildSurvivalText(SurvivalStats s)
    {
        StringBuilder sb = new();

        sb.AppendLine($"Max HP : {s.maxHP:0}");
        sb.AppendLine($"Regen : {s.hpRegen:0.##}");
        sb.AppendLine($"Armor : {s.armor:0.##}");
        sb.AppendLine($"Dodge : {s.dodgeChance:P0}");

        return sb.ToString();
    }

    private string BuildMovementText(MovementStats s)
    {
        StringBuilder sb = new();

        sb.AppendLine($"Move Speed : {s.moveSpeed:0.##}");

        return sb.ToString();
    }

    private string BuildUtilityText(UtilityStats s)
    {
        StringBuilder sb = new();

        sb.AppendLine($"EXP Gain : {s.expGain:P0}");
        sb.AppendLine($"Gold Gain : {s.goldGain:P0}");
        sb.AppendLine($"Pickup Range : {s.pickupRadius:0.##}");
        sb.AppendLine($"Luck : {s.luck:0.##}");
        sb.AppendLine($"Curse : {s.curse:0.##}");

        return sb.ToString();
    }
}