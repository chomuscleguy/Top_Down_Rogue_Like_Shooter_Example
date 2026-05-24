using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour, ITickable
{
    private readonly List<WeaponRuntime> runtimes = new();

    private IWeaponStatProvider statProvider;
    private Transform owner;
    private TargetScanner scanner;

    public List<WeaponRuntime> Runtimes => runtimes;

    public void Init(Transform owner, RunData runData, TargetScanner scanner, IWeaponStatProvider statProvider)
    {
        this.owner = owner;
        this.scanner = scanner;
        this.statProvider = statProvider;

        Build(runData.Items);
    }

    private void Build(ItemContainer items)
    {
        runtimes.Clear();

        foreach (var (item, level) in items.GetAllItems())
        {
            if (item is not WeaponData weaponData)
                continue;

            WeaponRuntime runtime = new WeaponRuntime(weaponData, level, owner, scanner, statProvider);

            runtime.RebuildStats(statProvider);

            runtimes.Add(runtime);
        }
    }

    public void AddOrLevelUpWeapon(WeaponData weaponData, int level)
    {
        WeaponRuntime runtime = FindRuntime(weaponData);

        if (runtime != null)
        {
            runtime.SetLevel(level);
            runtime.RebuildStats(statProvider);
            return;
        }

        runtime = new WeaponRuntime(weaponData, level, owner, scanner, statProvider);

        runtime.RebuildStats(statProvider);

        runtimes.Add(runtime);
    }

    private WeaponRuntime FindRuntime(WeaponData data)
    {
        for (int i = 0; i < runtimes.Count; i++)
        {
            if (runtimes[i].Data == data)
                return runtimes[i];
        }

        return null;
    }

    public void RebuildStats(IWeaponStatProvider provider)
    {
        statProvider = provider;

        for (int i = 0; i < runtimes.Count; i++)
        {
            runtimes[i].RebuildStats(provider);
        }
    }

    public void Tick(float dt)
    {
        for (int i = 0; i < runtimes.Count; i++)
        {
            runtimes[i].Tick(dt);
        }
    }
}