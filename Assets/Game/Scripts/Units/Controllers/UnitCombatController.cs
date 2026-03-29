using System;
using UnityEngine;
using UnityEngine.Events;

public class UnitCombatController : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent OnStartAttacking;
    public UnityEvent OnAttackPerformed;
    public UnityEvent OnStopAttacking;

    private UnitStats m_unitStats;
    private ITargetableUnit m_target;
    private UnitMovementController m_movementController;
    private CombatState m_state;

    private Coroutine m_timerCoroutine;

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

        m_state = CombatState.Idle;
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
        if (!InRange())
        {
            AttackTarget(m_target);
        }
    }

    private void HandleMovingState()
    {
        var targetTransform = m_target.GetPositon();

        if (targetTransform.hasChanged)
        {
            targetTransform.hasChanged = false;
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

        if (InRange())
        {
            HandleUnitArriveToLocation();
        }
        else
        {
            if (m_timerCoroutine != null)
            {
                StopCoroutine(m_timerCoroutine);
            }

            m_movementController.MoveTo(m_target.GetPositon().position, m_unitStats.Range.Value, HandleUnitArriveToLocation, false);

            m_state = CombatState.Moving;

            HandleState();
        }
    }

    private void HandleUnitArriveToLocation()
    {
        m_state = CombatState.Attacking;

        m_timerCoroutine = StartCoroutine(TimerHelper.TimerCoroutine(m_unitStats.AttackSpeed.Value, HandleAttackReady));

        m_movementController.LookToTarget(m_target.GetPositon().position);

        HandleState();

        OnStartAttacking?.Invoke();
    }

    private void HandleAttackReady()
    {
        OnAttackPerformed?.Invoke();
    }

    private bool InRange()
    {
        var targetTransform = m_target.GetPositon();
        var distanceFromTarget = Vector3.Distance(transform.position, targetTransform.position);

        return distanceFromTarget <= m_unitStats.Range.Value;
    }

    public void StopAttack(bool notify=true)
    {
        m_state = CombatState.Idle;

        if (m_timerCoroutine != null)
        {
            StopCoroutine(m_timerCoroutine);
        }

        if (notify)
        {
            OnStopAttacking?.Invoke();
        }

        HandleState();
    }

    public void ApplyHit(ITargetableUnit source)
    {
        if (m_target == null) return;

        m_target.TakeDamage(source, m_unitStats.Power.Value);

        if (!m_target.IsAlive())
        {
            StopAttack();
        }
    }

    public bool IsAttacking => m_state != CombatState.Idle;
}
