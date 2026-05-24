using System.Collections.Generic;
using UnityEngine;

public class OrbitWeaponObject : MonoBehaviour
{
    private Transform center;

    private float radius;
    private float rotateSpeed;
    private float angle;

    private float damage;

    private WeaponRuntime source;

    private readonly Dictionary<Collider2D, float> hitCooldowns = new();

    private float hitInterval = 0.3f;

    public void Init(Transform center, float radius, float rotateSpeed, float startAngle, float damage, WeaponRuntime source)
    {
        this.center = center;
        this.radius = radius;
        this.rotateSpeed = rotateSpeed;
        this.angle = startAngle;
        this.damage = damage;
        this.source = source;
    }

    private void Update()
    {
        if (center == null)
            return;

        angle += rotateSpeed * Time.deltaTime;

        float rad = angle * Mathf.Deg2Rad;

        Vector2 offset = new(Mathf.Cos(rad), Mathf.Sin(rad));

        transform.position = (Vector2)center.position + offset * radius;
    }

    private void LateUpdate()
    {
        UpdateCooldowns();
    }

    private void UpdateCooldowns()
    {
        var keys = new List<Collider2D>(hitCooldowns.Keys);

        foreach (var key in keys)
        {
            hitCooldowns[key] -= Time.deltaTime;

            if (hitCooldowns[key] <= 0)
                hitCooldowns.Remove(key);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (hitCooldowns.ContainsKey(other))
            return;

        var damageable =
            other.GetComponentInParent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage, source);
            hitCooldowns[other] = hitInterval;
        }
    }
}