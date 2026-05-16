using UnityEngine;

public class SceneUIBinder : MonoBehaviour
{
    [SerializeField] private HUDType hudType;

    private void Start()
    {
        Core.Instance.UI.ShowHUD(hudType);
    }
}