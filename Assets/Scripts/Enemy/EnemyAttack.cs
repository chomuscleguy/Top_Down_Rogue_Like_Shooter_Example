using UnityEngine;

public class EnemyAttack : MonoBehaviour, ITickable
{
    private float damage = 1f;
    private GameObject target;

    private float timer;
    private float interval = 0.5f;

    public void Init(EnemyData data)
    {
        damage = data.stats.combat.damage;
        Core.Instance.Tick.Register(this);
    }

    public void Tick(float dt)
    {
        if (target == null)
            return;

        timer += dt;

        if (timer < interval)
            return;

        timer = 0f;

        TryDealDamage();
    }

    private void TryDealDamage()
    {
        if (target == null)
            return;

        if (target.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(damage, null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target)
            target = null;
    }

    private void OnDestroy()
    {
        Core.Instance?.Tick.Unregister(this);
    }
}