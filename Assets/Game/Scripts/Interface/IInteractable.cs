using UnityEngine;

public interface IInteractable
{
    FactionSO GetFaction();
    bool IsActive();
    void Select();
    void Deselect();
    void HandleCommand(UnitCommand command);
}
