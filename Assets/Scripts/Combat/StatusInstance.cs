public abstract class StatusInstance
{
    protected float duration;
    protected float timer;

    protected Health target;

    public void Init(Health target, float duration)
    {
        this.target = target;
        this.duration = duration;
        timer = 0f;

        OnApply();
    }

    public bool Update(float deltaTime)
    {
        timer += deltaTime;

        OnUpdate(deltaTime);

        if (timer >= duration)
        {
            OnExpire();
            return true;
        }

        return false;
    }

    protected virtual void OnApply() { }
    protected virtual void OnUpdate(float dt) { }
    protected virtual void OnExpire() { }
}