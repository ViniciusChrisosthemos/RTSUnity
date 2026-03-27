using UnityEngine;

public class LocationCommand : UnitCommand
{
    public Vector3 Location { get; private set; }

    public LocationCommand(Vector3 location)
    {
        Location = location;
    }
}
