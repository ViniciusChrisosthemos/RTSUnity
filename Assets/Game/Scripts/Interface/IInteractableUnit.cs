using UnityEngine;

public interface IInteractableUnit
{
    FactionSO GetFaction();
    bool IsActive();
    void Select();
    void Deselect();
    void HandleCommand(UnitCommand command);
}
