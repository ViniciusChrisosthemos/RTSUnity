using UnityEngine;

public class UnitTargetCommand : UnitCommand
{
    public IInteractable Target {  get; private set; }

    public UnitTargetCommand(IInteractable target)
    {
        Target = target;
    }
}
