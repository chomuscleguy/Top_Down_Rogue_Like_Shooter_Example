using UnityEngine;

public class DropManager : MonoBehaviour, IManager
{
    [SerializeField] private ExpOrb small;
    [SerializeField] private ExpOrb medium;
    [SerializeField] private ExpOrb large;

    public void Init()
    {
        throw new System.NotImplementedException();
    }

    public void SpawnExp(int xp, Vector3 pos)
    {
        while (xp > 0)
        {
            ExpOrb prefab;

            if (xp >= 20) 
            {
                prefab = large;
                xp -= 20;
            }
            else if (xp >= 5)
            {
                prefab = medium; 
                xp -= 5;
            }
            else
            {
                prefab = small; 
                xp -= 1;
            }

            var orb = Instantiate(prefab, pos, Quaternion.identity);
            orb.Init(xp);
            Core.Instance.Orb.Register(orb);
        }
    }
}