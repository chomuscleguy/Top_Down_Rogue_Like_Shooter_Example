public class HPBar : BaseBar
{
    private IHealthProvider health;

    public void Init(IHealthProvider provider)
    {
        Unbind();

        health = provider;

        if (health != null)
        {
            health.OnHealthChanged += SetValue;

            SetValue(health.CurrentHP, health.MaxHP);
        }
    }

    public void Unbind()
    {
        if (health != null)
            health.OnHealthChanged -= SetValue;

        health = null;
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