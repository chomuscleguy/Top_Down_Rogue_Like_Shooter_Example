using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public HPBar hpBar;
    public ExpBar expBar;

    private IHealthProvider health;
    private IExperienceProvider exp;

    public void Init(IHealthProvider healthProvider, IExperienceProvider expProvider)
    {
        if (health != null)
            hpBar.Unbind();

        if (exp != null)
            expBar.Unbind();

        health = healthProvider;
        exp = expProvider;

        hpBar.Init(health);
        expBar.Init(exp);
    }

    private void OnDisable()
    {
        hpBar?.Unbind();
        expBar?.Unbind();
    }
}