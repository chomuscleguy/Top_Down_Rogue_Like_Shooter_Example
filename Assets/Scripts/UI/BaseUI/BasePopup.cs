using UnityEngine;

public abstract class BasePopup : MonoBehaviour
{
    public bool IsOpen { get; private set; }

    public void Open()
    {
        if (IsOpen)
            return;

        IsOpen = true;

        gameObject.SetActive(true);

        Init();
    }

    protected virtual void Init() { }

    public void Close()
    {
        if (!IsOpen)
            return;

        IsOpen = false;

        gameObject.SetActive(false);
    }
}