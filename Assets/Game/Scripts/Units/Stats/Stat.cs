using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stat
{
    public float BaseValue {  get; private set; }
    public List<StatModifier> Modifiers { get; private set; }

    public Stat(float baseValue, List<StatModifier> modifiers)
    {
        BaseValue = baseValue;
        Modifiers = modifiers;
    }

    public float Value => BaseValue + Modifiers.Sum(modifier => modifier.ModifierType == ModifierType.Flat ? modifier.Value : modifier.Value * BaseValue);
}
