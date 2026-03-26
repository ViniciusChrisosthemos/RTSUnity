using UnityEngine;

public class Faction
{
    public string ID { get; set; }
    public string Name { get; private set; }

    public Faction(string iD, string name)
    {
        ID = iD;
        Name = name;
    }
}
