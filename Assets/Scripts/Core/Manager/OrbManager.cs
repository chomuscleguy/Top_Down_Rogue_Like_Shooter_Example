using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour, IManager
{
    [SerializeField] private Transform player;
    [SerializeField] private float magnetRange = 3f;

    private readonly List<ExpOrb> orbs = new();

    public void SetPlayer(Transform p)
    {
        player = p;
    }

    public void Register(ExpOrb orb)
    {
        if (!orbs.Contains(orb))
            orbs.Add(orb);
    }

    public void Unregister(ExpOrb orb)
    {
        orbs.Remove(orb);
    }

    private void Update()
    {
        if (player == null)
            return;

        float dt = Time.deltaTime;

        Vector3 pPos = player.position;
        float magnetSqr = magnetRange * magnetRange;

        for (int i = 0; i < orbs.Count; i++)
        {
            var orb = orbs[i];
            if (orb == null) continue;

            Vector3 dir = pPos - orb.transform.position;
            float sqrDist = dir.sqrMagnitude;

            // 1️⃣ Magnet 영역 진입 → 추적 시작
            if (!orb.isFollowing && sqrDist < magnetSqr)
            {
                orb.Activate();
            }

            if (!orb.isFollowing)
                continue;

            // 2️⃣ 흡수 판정
            if (sqrDist < 0.04f)
            {
                Collect(orb);
                continue;
            }

            // 3️⃣ 이동
            float dist = Mathf.Sqrt(sqrDist);
            dir /= dist;

            float speed = orb.speed + sqrDist * 5f;

            orb.transform.position += dir * speed * dt;
        }
    }

    private void Collect(ExpOrb orb)
    {
        var exp = player.GetComponent<Experience>();
        if (exp != null)
            exp.AddXP(orb.xp);

        Unregister(orb);
        orb.Deactivate();
    }

    public void Init()
    {
        throw new System.NotImplementedException();
    }
}