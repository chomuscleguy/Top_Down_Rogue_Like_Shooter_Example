using UnityEngine;
using UnityEngine.UI;

public class PopupButton : MonoBehaviour
{
    [SerializeField] private UIType type;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Open);
    }

    private void Open()
    {
        Core.Instance.UI.ShowPopup<BasePopup>(type);
    }
}