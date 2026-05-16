public class HPBar : BaseBar
{
    private IHealthProvider health;

    public void Init(IHealthProvider provider)
    {
        health = provider;

        health.OnHealthChanged += SetValue;
    }

    private void OnDisable()
    {
        if (health != null)
            health.OnHealthChanged -= SetValue;
    }
}