using UnityEngine;

public class EnemyAttack : MonoBehaviour, ITickable
{
    [SerializeField]
    private float damage = 1f;

    private GameObject target;

    private float timer;
    private float interval = 1f;

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
        bool critical = false;

        HitContext hit = Core.Instance.Combat.CreateHit(attacker: gameObject, target: target, baseDamage: damage, 
            direction: (target.transform.position - transform.position).normalized, isCritical: critical);

        Core.Instance.Combat.ProcessHit(hit);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            target = collision.gameObject;
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