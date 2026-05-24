using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour, ITickable
{
    [Header("Behaviour")]
    [SerializeField]
    private List<MeleeBehaviour> behaviours = new();
    public void Tick(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    private void OnDestroy()
    {
        Core.Instance.Tick.Unregister(this);
    }
}