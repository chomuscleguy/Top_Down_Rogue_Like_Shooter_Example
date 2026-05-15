using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    public int xp;
    public bool isFollowing;
    public float speed = 3f;

    public void Init(int xp)
    {
        this.xp = xp;
        isFollowing = false;
    }

    public void Activate()
    {
        isFollowing = true;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}