using UnityEngine;

public class Experience : MonoBehaviour
{
    private RunData run;

    public void Init(RunData run)
    {
        this.run = run;
    }

    public void AddXP(int amount)
    {
        run.AddXP(amount);
    }
}