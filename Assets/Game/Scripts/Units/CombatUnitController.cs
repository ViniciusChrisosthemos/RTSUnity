using UnityEngine;

[RequireComponent(typeof(UnitCombatController), typeof(AnimationTriggersHelper))]
public class CombatUnitController : BaseUnitController
{
    private UnitCombatController m_combatController;
    private AnimationTriggersHelper m_animationTriggersHelper;

    protected override void HandleAwake()
    {
        m_combatController = GetComponent<UnitCombatController>();
        m_animationTriggersHelper = GetComponent<AnimationTriggersHelper>();

        m_combatController.OnStartAttacking.AddListener(HandleStartAttacking);
        m_combatController.OnAttackPerformed.AddListener(HandleAttackReady);
        m_combatController.OnStopAttacking.AddListener(HandleStopAttacking);

        m_animationTriggersHelper.OnAttackHitTarget.AddListener(HandleAttackHitTarget);
    }

    public override void HandleCommand(UnitCommand command)
    {
        switch(command)
        {
            case LocationCommand locationCommand: MoveToLocation(locationCommand.Location); break;
            case UnitTargetCommand targetCommand: HandleTargetCommand(targetCommand.Target); break;
            default: break;
        }
    }

    private void HandleTargetCommand(IInteractableUnit unit)
    {
        if (unit.GetFaction() == GetFaction()) return;

        m_combatController.AttackTarget((ITargetableUnit)unit);
    }

    protected override void HandleTakeDamage(ITargetableUnit source, float damage)
    {
        if (m_combatController.IsAttacking || !IsAlive()) return;

        m_combatController.AttackTarget(source);
    }

    protected override void HandleMoveToLocation(Vector3 position)
    {
        m_combatController.StopAttack(false);
    }

    protected override void HandleKillUnit()
    {
        m_combatController.StopAttack(false);
    }

    protected override void HandleSetup()
    {
        m_combatController.Setup(m_stats, m_unitMovementController);
    }

    private void HandleStartAttacking()
    {
        m_animationController.PlayCombatInstanceAnimation();
    }

    private void HandleAttackReady()
    {
        m_animationController.PlayAttackingAnimation();
    }

    private void HandleStopAttacking()
    {
        m_animationController.PlayIdleAnimation();
    }

    private void HandleAttackHitTarget()
    {
        m_combatController.ApplyHit(this);
    }
}
