using UnityEngine;
using System.Collections.Generic;

public class TickSystem : MonoBehaviour
{
    private readonly HashSet<ITickable> tickables = new();

    private readonly List<ITickable> tickBuffer = new();

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
            tickable.Tick(dt);
        }
    }
}