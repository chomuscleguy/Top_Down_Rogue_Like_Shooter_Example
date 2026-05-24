using TMPro;
using UnityEngine;

public class ResultPlayerInfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playTime;
    [SerializeField] private TextMeshProUGUI goldEarn;
    [SerializeField] private TextMeshProUGUI levelReached;
    [SerializeField] private TextMeshProUGUI kills;

    public void Init()
    {
        var run = Core.Instance.Game.Run;

        playTime.text = run.PlayTime.ToString();
        goldEarn.text = run.Gold.ToString();
        levelReached.text = run.Level.ToString();
        kills.text = run.KillCount.ToString();
    }
}
