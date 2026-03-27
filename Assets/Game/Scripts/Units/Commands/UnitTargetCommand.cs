using UnityEngine;

public class UnitTargetCommand : UnitCommand
{
    public IInteractableUnit Target {  get; private set; }

    public UnitTargetCommand(IInteractableUnit target)
    {
        Target = target;
    }
}
