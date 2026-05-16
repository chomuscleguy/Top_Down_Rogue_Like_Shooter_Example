using System.Collections.Generic;

public class StatCalculatorService
{
    private List<IStatModifier> modifiers = new();

    public void Clear()
    {
        modifiers.Clear();
    }

    public void AddModifier(IStatModifier mod)
    {
        modifiers.Add(mod);
    }

    public CharacterData Calculate(CharacterData data)
    {
        var result = data;

        foreach (var mod in modifiers)
        {
            mod.Apply(ref result.stats);
        }

        return result;
    }
}