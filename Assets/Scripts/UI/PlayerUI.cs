using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public HPBar hpBar;
    public ExpBar expBar;

    public void Init(IHealthProvider health, IExperienceProvider exp)
    {
        hpBar.Init(health);
        expBar.Init(exp);
    }
}