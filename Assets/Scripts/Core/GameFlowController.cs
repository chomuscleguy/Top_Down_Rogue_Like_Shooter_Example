using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    private RunData run;
    private Health playerHealth;

    public void Init(RunData runData, Health health)
    {
        run = runData;
        playerHealth = health;

        run.OnLevelUp += HandleLevelUp;
        playerHealth.OnDeath += HandleGameOver;
    }

    private void HandleLevelUp()
    {
        Pause();

        Core.Instance.UI.ShowPopup<LevelUpPopup>(UIType.LevelUpPopup);
    }

    private void HandleGameOver(Health _)
    {
        Pause();

        Core.Instance.UI.ShowPopup<ResultPopupUI>(UIType.GameOverPopup);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        if (run != null)
            run.OnLevelUp -= HandleLevelUp;

        if (playerHealth != null)
            playerHealth.OnDeath -= HandleGameOver;
    }
}