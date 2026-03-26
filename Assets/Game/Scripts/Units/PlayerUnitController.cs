using UnityEngine;

[RequireComponent(typeof(UnitMovementController),typeof(UnitAnimationController),typeof(UnitSelectionView))]
[RequireComponent(typeof(UnitCombatController))]
public class PlayerUnitController : MonoBehaviour, IInteractableUnit
{
    private Faction m_faction;
    private UnitStats m_unitStats;
    private UnitMovementController m_movementController;
    private UnitAnimationController m_animationController;
    private UnitSelectionView m_selectionView;
    private UnitCombatController m_combatController;

    private void Awake()
    {
        m_selectionView = GetComponent<UnitSelectionView>();    
        m_movementController = GetComponent<UnitMovementController>();
        m_animationController = GetComponent<UnitAnimationController>();
        m_combatController = GetComponent<UnitCombatController>();

        m_movementController.OnStartMove.AddListener(HandleStartMove);
        m_movementController.OnStopMove.AddListener(HandleStopMove);
    }

    public void Deselect()
    {
        m_selectionView.Deselect();
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
        m_selectionView.Select();
    }

    public void Setup(UnitStats unitStat, Faction faction)
    {
        m_faction = faction;
        m_unitStats = unitStat;
        m_movementController.SetSpeed(m_unitStats.Speed.Value);
    }

    private void HandleStartMove(UnitMovementController controller, Vector3 targetPosition)
    {
        m_animationController.PlayMovingAnimation();
    }

    private void HandleStopMove(UnitMovementController controller)
    {
        m_animationController.PlayIdleAnimation();
    }

    public void AttackUnit(ITargetableUnit unit)
    {
        m_combatController.AttackTarget(unit);
    }
}
