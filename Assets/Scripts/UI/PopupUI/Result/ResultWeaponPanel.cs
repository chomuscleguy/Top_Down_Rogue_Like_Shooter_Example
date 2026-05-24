using System.Collections.Generic;

public class ResultWeaponPanel : BaseListUI<WeaponRuntime, ResultWeaponSlot>
{
    public void Init(List<WeaponRuntime> runtimes)
    {
        Rebuild(runtimes, Bind);
    }

    private void Bind(ResultWeaponSlot slot, WeaponRuntime runtime)
    {
        slot.Init(runtime);
    }
}