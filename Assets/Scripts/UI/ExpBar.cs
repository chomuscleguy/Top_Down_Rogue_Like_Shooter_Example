using TMPro;
using UnityEngine;

public class ExpBar : BaseBar
{
    private IExperienceProvider exp;

    [SerializeField] private TextMeshProUGUI levelText;

    public void Init(IExperienceProvider provider)
    {
        Unbind();

        exp = provider;

        if (exp != null)
        {
            exp.OnExpChanged += HandleExpChanged;
            exp.OnLevelChanged += HandleLevelChanged;

            HandleLevelChanged(exp.Level);
            SetValue(exp.CurrentXP, exp.XPToNextLevel);
        }
    }

    private void HandleExpChanged(int current, int max)
    {
        SetValue(current, max);
    }

    private void HandleLevelChanged(int lv)
    {
        levelText.text = $"Lv.{lv}";
    }

    public void Unbind()
    {
        if (exp != null)
        {
            exp.OnExpChanged -= HandleExpChanged;
            exp.OnLevelChanged -= HandleLevelChanged;
        }

        exp = null;
    }

    private void OnDisable()
    {
        Unbind();
    }

    private void OnDestroy()
    {
        Unbind();
    }
}