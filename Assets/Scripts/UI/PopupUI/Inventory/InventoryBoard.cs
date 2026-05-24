using System.Collections.Generic;
using UnityEngine;

public class InventoryBoard : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField]
    private List<InventorySlot> weaponSlots;

    [Header("Passive")]
    [SerializeField]
    private List<InventorySlot> passiveSlots;

    private RunData run;

    public void Init(RunData runData)
    {
        run = runData;

        run.OnInventoryChanged += Refresh;

        Refresh();
    }

    private void Refresh()
    {
        RefreshWeapons();
        RefreshPassives();
    }

    private void RefreshWeapons()
    {
        int index = 0;

        foreach (var (item, level)
                 in run.Items.GetAllItems())
        {
            if (item is not WeaponData)
                continue;

            if (index >= weaponSlots.Count)
                break;

            weaponSlots[index].Set(item, level);

            index++;
        }

        for (; index < weaponSlots.Count; index++)
        {
            weaponSlots[index].Clear();
        }
    }

    private void RefreshPassives()
    {
        int index = 0;

        foreach (var (item, level)
                 in run.Items.GetAllItems())
        {
            if (item is WeaponData)
                continue;

            if (index >= passiveSlots.Count)
                break;

            passiveSlots[index].Set(item, level);

            index++;
        }

        for (; index < passiveSlots.Count; index++)
        {
            passiveSlots[index].Clear();
        }
    }

    private void OnDestroy()
    {
        if (run != null)
        {
            run.OnInventoryChanged -= Refresh;
        }
    }
}