using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour
{
    private List<StatusInstance> active = new();

    public void Apply(IStatusEffect effect, Health target)
    {
        var instance = effect.CreateInstance();
        instance.Init(target, GetDuration(effect));

        active.Add(instance);
    }

    private float GetDuration(IStatusEffect effect)
    {
        return 3f; // 기본값 or effect에서 가져오기
    }

    private void Update()
    {
        for (int i = active.Count - 1; i >= 0; i--)
        {
            if (active[i].Update(Time.deltaTime))
                active.RemoveAt(i);
        }
    }
}