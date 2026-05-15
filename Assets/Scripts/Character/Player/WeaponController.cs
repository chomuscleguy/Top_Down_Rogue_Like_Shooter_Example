using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour,ITickable
{
    private readonly Dictionary<WeaponData, Weapon> weapons = new();

    private RunData runData;

    public void Init(RunData data)
    {
        runData = data;

        runData.OnItemAdded += HandleItemAdded;

        foreach (var (item, level) in runData.Items.GetAllItems())
        {
            if (item is WeaponData weapon)
                AddWeapon(weapon);
        }


        Core.Instance.Tick.Register(this);
    }

    private void HandleItemAdded(ItemData item)
    {
        if (item is WeaponData weapon)
        {
            AddWeapon(weapon);

            ApplyStatsToAll(
                runData,
                runData.Items);
        }
    }
    public void AddWeapon(WeaponData data)
    {
        if (weapons.ContainsKey(data))
            return;

        if (data.weaponPrefab == null)
            return;

        Weapon weapon =   Instantiate(data.weaponPrefab, transform);

        weapon.Init(data);

        weapons.Add(data, weapon);
    }

    public void RemoveWeapon(WeaponData data)
    {
        if (!weapons.TryGetValue(data, out Weapon weapon))
            return;

        Destroy(weapon.gameObject);
        weapons.Remove(data);
    }

    public void ApplyStatsToAll( IStatQuery stats, ItemContainer items)
    {
        foreach (var pair in weapons)
        {
            int level = items.GetLevel(pair.Key);

            pair.Value.SetStats(stats, level);
        }
    }

    public void Tick(float dt)
    {
        foreach (var pair in weapons)
        {
            pair.Value.Tick(dt);
        }
    }

    private void OnDestroy()
    {
        if (runData != null)
            runData.OnItemAdded -= HandleItemAdded;

        Core.Instance.Tick.Unregister(this);
    }
}