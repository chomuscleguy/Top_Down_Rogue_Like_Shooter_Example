using UnityEngine;
using UnityEngine.UI;

public class ClosePopupButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Close);
    }

    private void Close()
    {
        Core.Instance.UI.CloseTopPopup();
    }
}
