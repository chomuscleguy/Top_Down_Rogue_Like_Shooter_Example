using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    private RunData run;

    public void Init(RunData runData)
    {
        run = runData;

        run.OnLevelUp += HandleLevelUp;
        run.OnDeath += HandleGameOver;
    }

    private void HandleLevelUp()
    {
        Pause();

        var items = ItemSelector.GetRandomSelectableItems(run, Core.Instance.Data.Items, 3);

        var ui = Core.Instance.UI.ShowPopup<LevelUpPopup>(UIType.LevelUpPopup);

        ui.Init(items, data => run.Items.GetLevel(data), OnItemSelected);
    }

    private void OnItemSelected(ItemData data)
    {
        run.AddItem(data);

        Core.Instance.UI.CloseTopPopup();

        Resume();
    }

    private void HandleGameOver()
    {
        Pause();
        Core.Instance.UI.ShowPopup<BasePopup>(UIType.GameOverPopup);
    }

    public void Pause() => Time.timeScale = 0f;
    public void Resume() => Time.timeScale = 1f;

    private void OnDestroy()
    {
        if (run == null) return;

        run.OnLevelUp -= HandleLevelUp;
        run.OnDeath -= HandleGameOver;
    }
}