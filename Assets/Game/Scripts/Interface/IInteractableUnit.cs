using UnityEngine;

public interface IInteractableUnit
{
    void AttackUnit(ITargetableUnit unit);
    void Select();
    void Deselect();
    void MoveTo(Vector3 position);
    bool IsTarget(Faction targetFaction);
}
