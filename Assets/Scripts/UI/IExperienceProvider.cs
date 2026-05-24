using System;

public interface IExperienceProvider
{
    int Level { get; }
    int CurrentXP { get; }
    int XPToNextLevel { get; }

    event Action<int, int> OnExpChanged;
    event Action<int> OnLevelChanged;
    event Action OnLevelUp;
}