using UnityEngine;

public class StatModifier
{
    public float Value {  get; private set; }
    public ModifierType ModifierType { get; private set; }
    
    public StatModifier(float value, ModifierType modifierType)
    {
        Value = value;
        ModifierType = modifierType;
    }
}

public enum ModifierType
{
    Flat,
    Perc
}
