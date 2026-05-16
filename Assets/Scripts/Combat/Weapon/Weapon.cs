using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponData BaseData { get; private set; }

    protected WeaponStats RuntimeStat;
    protected TargetScanner Scanner;

    protected int CurrentLevel;

    private float timer;

    public virtual void Init(WeaponData data)
    {
        BaseData = data;

        Scanner = GetComponentInParent<TargetScanner>();

        timer = 0f;
    }

    public virtual void SetStats(IStatQuery stats, int level)
    {
        CurrentLevel = level;

        var levelData = BaseData.GetLevelData(level);

        if (levelData == null)
            return;

        RuntimeStat = WeaponStatCalculator.Calculate(levelData, stats);
    }

    public virtual void Tick(float deltaTime)
    {
        if (CurrentLevel <= 0)
            return;

        timer += deltaTime;

        if (timer >= RuntimeStat.attackInterval)
        {
            timer = 0f;
            Attack();
        }
    }

    protected abstract void Attack();
}