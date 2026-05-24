using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;

    public int Value { get; private set; }

    public bool IsFollowing { get; private set; }

    public float Speed => speed;

    public void Init(int value)
    {
        Value = value;

        IsFollowing = false;

        gameObject.SetActive(true);
    }

    public void Activate()
    {
        IsFollowing = true;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public abstract void Collect(Player player);
}