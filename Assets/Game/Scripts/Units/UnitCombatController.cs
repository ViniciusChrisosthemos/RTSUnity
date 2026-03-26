using UnityEngine;

public class UnitCombatController : MonoBehaviour
{
    private UnitStats m_unitStats;
    private ITargetableUnit m_target;
    private UnitMovementController m_movementController;
    private CombatState m_state;

    private enum CombatState
    {
        Attacking,
        Moving,
        Idle
    }

    public void Setup(UnitStats unitStats, UnitMovementController movementController)
    {
        m_unitStats = unitStats;
        m_movementController = movementController;
    }

    private void Update()
    {
        if (m_target != null)
        {
            HandleState();
        }
    }

    private void HandleState()
    {
        switch (m_state)
        {
            case CombatState.Idle: HandleIdleState(); break;
            case CombatState.Moving: HandleMovingState(); break;
            case CombatState.Attacking: HandleAttackingState(); break;
            default: break;
        }
    }

    private void HandleAttackingState()
    {
        if (!m_target.IsAlive())
        {
            m_state = CombatState.Idle;
            return;
        }

        if (!InRange())
        {
            AttackTarget(m_target);
        }
    }

    private void HandleMovingState()
    {
        if (InRange())
        {
            m_state = CombatState.Attacking;
            return;
        }

        var targetTransform = m_target.GetPositon();

        if (targetTransform.hasChanged)
        {
            AttackTarget(m_target);
        }
    }

    private void HandleIdleState()
    {
        m_target = null;
    }

    public void AttackTarget(ITargetableUnit target)
    {
        m_target = target;

        m_movementController.MoveTo(m_target.GetPositon().position, 0.1f, HandleUnitArriveToLocation);

        m_state = CombatState.Moving;
    }

    private void HandleUnitArriveToLocation()
    {
        m_state = CombatState.Attacking;
    }

    private bool InRange()
    {
        var targetTransform = m_target.GetPositon();
        var distanceFromTarget = Vector3.Distance(transform.position, targetTransform.position);

        return distanceFromTarget <= m_unitStats.Range.Value;
    }
}
