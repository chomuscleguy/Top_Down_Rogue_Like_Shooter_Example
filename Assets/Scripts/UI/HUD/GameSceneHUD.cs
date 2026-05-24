using TMPro;
using UnityEngine;

public class GameSceneHUD : BaseHUD
{
    [SerializeField] private InventoryBoard inventoryBoard;
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI timeText;

    private RunData run;

    public override void Bind()
    {
        run = Core.Instance.Game.Run;

        run.OnKillChanged += UpdateKill;
        run.OnGoldChanged += UpdateGold;
        run.OnTimeChanged += UpdateTime;

        inventoryBoard.Init(run);
        UpdateKill(run.KillCount);
        UpdateGold(run.Gold);
        UpdateTime(run.PlayTime);
    }

    private void UpdateKill(int kill)
    {
        killText.text = $"Kill {kill}";
    }

    private void UpdateGold(int gold)
    {
        goldText.text = gold.ToString();
    }

    private void UpdateTime(float time)
    {
        int min = Mathf.FloorToInt(time / 60f);
        int sec = Mathf.FloorToInt(time % 60f);

        timeText.text = $"{min:00}:{sec:00}";
    }

    private void OnDestroy()
    {
        if (run == null)
            return;

        run.OnKillChanged -= UpdateKill;
        run.OnGoldChanged -= UpdateGold;
        run.OnTimeChanged -= UpdateTime;
    }
}