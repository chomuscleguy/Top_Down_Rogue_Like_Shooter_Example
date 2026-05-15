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

        OnOpen();
    }

    public void Close()
    {
        if (!IsOpen)
            return;

        IsOpen = false;

        OnClose();

        gameObject.SetActive(false);
    }

    protected virtual void OnOpen() { }
    protected virtual void OnClose() { }
}