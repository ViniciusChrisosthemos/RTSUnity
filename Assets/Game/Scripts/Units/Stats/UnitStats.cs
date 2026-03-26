using UnityEngine;

public class UnitStats
{
    public Stat Health { get; private set; }
    public Stat Power { get; private set; }
    public Stat Defense { get; private set; }
    public Stat Speed { get; private set; }
    public Stat AttackSpeed { get; private set; }
    public Stat Range { get; private set; }

    public UnitStats(float baseHealth, float basePower, float baseDefense, float baseSpeed, float baseAttackSpeed, float baseRange)
    {
        Health = new Stat(baseHealth, new());
        Power = new Stat(basePower, new());
        Defense = new Stat(baseDefense, new());
        Speed = new Stat(baseSpeed, new());
        AttackSpeed = new Stat(baseAttackSpeed, new());
        Range = new Stat(baseRange, new());
    }

    public UnitStats(Stat health, Stat power, Stat defense, Stat speed, Stat attackSpeed, Stat range)
    {
        Health = health;
        Power = power;
        Defense = defense;
        Speed = speed;
        AttackSpeed = attackSpeed;
        Range = range;
    }
}
