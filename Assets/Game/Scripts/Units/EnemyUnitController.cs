using UnityEngine;

[RequireComponent(typeof(UnitMovementController), typeof(UnitAnimationController))]
public class EnemyUnitController : MonoBehaviour, IInteractableUnit, ITargetableUnit
{
    private UnitMovementController m_movementController;
    private UnitAnimationController m_animationController;

    private Faction m_faction;

    public EnemyUnitController(UnitStats stats, Faction faction)
    {

    }

    public void Attack(float damage)
    {

    }

    public void AttackUnit(ITargetableUnit unit)
    {

    }

    public void Deselect()
    {

    }

    public Transform GetPositon()
    {
        return transform;
    }

    public bool IsAlive()
    {
        return true;
    }

    public bool IsTarget(Faction targetFaction)
    {
        return targetFaction != m_faction;
    }

    public void MoveTo(Vector3 position)
    {
        m_movementController.MoveTo(position);
    }

    public void Select()
    {
        throw new System.NotImplementedException();
    }
}
