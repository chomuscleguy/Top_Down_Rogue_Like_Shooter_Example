using UnityEngine;

public class HitFlash : MonoBehaviour
{
    [Header("Flash")]
    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private Color hitColor = Color.red;

    [SerializeField]
    private float duration = 0.1f;

    private Color defaultColor;

    private float timer;

    private void Awake()
    {
        if (sprite == null)
            sprite = GetComponentInChildren<SpriteRenderer>();

        if (sprite != null)
            defaultColor = sprite.color;
    }

    private void Update()
    {
        if (timer <= 0f)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            ResetColor();
        }
    }

    public void Play()
    {
        if (sprite == null)
            return;

        sprite.color = hitColor;

        timer = duration;
    }

    private void ResetColor()
    {
        if (sprite == null)
            return;

        sprite.color = defaultColor;
    }

    private void OnDisable()
    {
        timer = 0f;

        ResetColor();
    }
}