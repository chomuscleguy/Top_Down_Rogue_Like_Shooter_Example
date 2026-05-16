using TMPro;
using UnityEngine;

public class LobbyHUD : BaseHUD
{
    [SerializeField] private TextMeshProUGUI goldText;

    private PlayerProgress progress;

    public override void Bind()
    {
        progress = Core.Instance.Game.Progress;

        progress.OnGoldChanged += UpdateGold;

        UpdateGold(progress.Gold);
    }

    private void UpdateGold(int gold)
    {
        goldText.text = gold.ToString();
    }

    private void OnDestroy()
    {
        if (progress == null)
            return;

        progress.OnGoldChanged -= UpdateGold;
    }
}