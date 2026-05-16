public class ExpBar : BaseBar
{
    private IExperienceProvider exp;

    public void Init(IExperienceProvider provider)
    {
        Unbind();

        exp = provider;

        if (exp != null)
            exp.OnExpChanged += HandleExpChanged;
    }

    private void HandleExpChanged(int current, int max)
    {
        SetValue(current, max);
    }

    private void Unbind()
    {
        if (exp != null)
        {
            exp.OnExpChanged -= HandleExpChanged;
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