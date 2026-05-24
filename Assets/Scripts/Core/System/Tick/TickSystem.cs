using UnityEngine;
using System.Collections.Generic;

public class TickSystem : MonoBehaviour, IManager
{
    private readonly HashSet<ITickable> tickables = new();

    private readonly List<ITickable> tickBuffer = new();

    public void Init()
    {

    }

    public void Register(ITickable tickable)
    {
        if (tickable == null)
            return;

        tickables.Add(tickable);
    }

    public void Unregister(ITickable tickable)
    {
        tickables.Remove(tickable);
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        tickBuffer.Clear();
        tickBuffer.AddRange(tickables);

        foreach (ITickable tickable in tickBuffer)
        {
            if (tickable == null)
            {
                tickables.Remove(tickable);
                continue;
            }

            tickable.Tick(dt);
        }
    }

    public void Clear()
    {
        tickables.Clear();
    }
}