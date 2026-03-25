using UnityEngine;

[RequireComponent(typeof(UnitMovementController), typeof(UnitAnimationController), typeof(UnitSelectionView))]
public class PlayerUnitController : MonoBehaviour, IInteractableUnit
{
    private UnitStats m_unitStats;
    private UnitMovementController m_movementController;
    private UnitAnimationController m_animationController;
    private UnitSelectionView m_selectionView;

    public void Deselect()
    {
        m_selectionView.Deselect();
    }

    public void MoveTo(Vector3 position)
    {
        m_movementController.MoveTo(position);
    }

    public void Select()
    {
        m_selectionView.Select();
    }

    public void Setup(UnitStats unitStat)
    {
        m_unitStats = unitStat;
        m_movementController.SetSpeed(m_unitStats.Speed.Value);

        m_movementController.OnStartMove.AddListener(HandleStartMove);
        m_movementController.OnStopMove.AddListener(HandleStopMove);
    }

    private void HandleStartMove(UnitMovementController controller, Vector3 targetPosition)
    {
        m_animationController.PlayMovingAnimation();
    }

    private void HandleStopMove(UnitMovementController controller)
    {
        m_animationController.PlayIdleAnimation();
    }
}
