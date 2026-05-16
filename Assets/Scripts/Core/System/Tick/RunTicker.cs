using UnityEngine;

public class RunTicker : MonoBehaviour, ITickable
{
    private RunData run;

    public void Init(RunData runData)
    {
        run = runData;

        Core.Instance.Tick.Register(this);
    }

    public void Tick(float deltaTime)
    {
        run?.Tick(deltaTime);
    }

    private void OnDestroy()
    {
        if (Core.Instance != null)
        {
            Core.Instance.Tick.Unregister(this);
        }
    }
}