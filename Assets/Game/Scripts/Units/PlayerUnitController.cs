using UnityEngine;

[RequireComponent(typeof(UnitMovementController), typeof(UnitAnimationController))]
public class PlayerUnitController : MonoBehaviour
{
    private UnitStats m_unitStats;
    private UnitMovementController m_movementController;
    private UnitAnimationController m_animationController;

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
