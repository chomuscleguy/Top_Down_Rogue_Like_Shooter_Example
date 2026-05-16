using System.Collections;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    protected ObjectPool<Projectile> Pool;

    protected ProjectileWeaponData ProjectileData;

    private bool isBursting;

    public override void Init(WeaponData data)
    {
        base.Init(data);

        ProjectileData = data as ProjectileWeaponData;

        if (ProjectileData == null)
            return;

        Pool = new ObjectPool<Projectile>();

        Pool.Init(ProjectileData.projectilePrefab, 20, transform);
    }

    protected override void Attack()
    {
        if (isBursting)
            return;

        StartCoroutine(BurstRoutine());
    }

    private IEnumerator BurstRoutine()
    {
        isBursting = true;

        for (int i = 0; i < RuntimeStat.projectileCount; i++)
        {
            FireOnce();

            yield return new WaitForSeconds(0.05f);
        }

        isBursting = false;
    }

    private void FireOnce()
    {
        Transform target = null;

        target = ProjectileData.targetingMode == TargetingMode.Closest ? Scanner.GetClosestTarget() : Scanner.GetRandomTarget();

        if (target == null)
            return;

        Vector2 dir = (target.position - transform.position).normalized;

        if (RuntimeStat.spreadAngle > 1)
        {
            FireSpread(dir, target);
        }
        else
        {
            Fire(dir, target);
        }
    }

    protected virtual void Fire(Vector2 dir, Transform target = null)
    {
        Projectile projectile = Pool.Get();

        Vector2 spawnPos = transform.position;

        if (RuntimeStat.spreadAngle <= 1f)
        {
            Vector2 side = Vector2.Perpendicular(dir).normalized;

            float randomOffset = Random.Range(-0.2f, 0.2f);

            spawnPos += side * randomOffset;
        }

        projectile.transform.position = spawnPos;

        projectile.Init(dir, RuntimeStat, Pool);

        projectile.Target = target;
    }

    protected virtual void FireSpread(Vector2 baseDir, Transform target = null)
    {
        int count = Mathf.Max(1, RuntimeStat.projectileCount);

        float totalAngle = RuntimeStat.spreadAngle;

        float step = count > 1 ? totalAngle / (count - 1) : 0f;

        for (int i = 0; i < count; i++)
        {
            float offset = -totalAngle * 0.5f + step * i;

            Vector2 dir = Quaternion.Euler(0, 0, offset) * baseDir;

            Fire(dir, target);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        isBursting = false;
    }
}