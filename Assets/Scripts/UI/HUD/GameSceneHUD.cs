using System;
using TMPro;
using UnityEngine;

public class GameSceneHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI timeText;

    private RunData run;

    public void Init(RunData runData)
    {
        run = runData;

        run.OnKillChanged += UpdateKill;
        run.OnGoldChanged += UpdateGold;
        run.OnTimeChanged += UpdateTime;

        UpdateKill(run.KillCount);
        UpdateGold(run.Gold);
    }

    private void UpdateKill(int kill)
    {
        killText.text = $"Kill {kill}";
    }

    private void UpdateGold(int gold)
    {
        goldText.text = $"{gold}";
    }

    private void UpdateTime(float time)
    {
        int min = Mathf.FloorToInt(time / 60f);
        int sec = Mathf.FloorToInt(time % 60f);

        timeText.text = $"{min:00}:{sec:00}";
    }

    private void OnDestroy()
    {
        if (run == null) return;

        run.OnKillChanged -= UpdateKill;
        run.OnGoldChanged -= UpdateGold;
        run.OnTimeChanged -= UpdateTime;
    }

    
}
