using System.Collections;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(UnitMovementController), typeof(UnitSelectionView), typeof(UnitAnimationController))]
public abstract class BaseUnitController : MonoBehaviour, IInteractableUnit, ITargetableUnit
{
    [SerializeField] private HealthBarView m_healthBarView;

    [Header("Parameters")]
    [SerializeField] private float m_timeToDestroySafety = 3f;

    protected UnitMovementController m_unitMovementController;
    protected UnitSelectionView m_unitSelectionView;
    protected UnitAnimationController m_animationController;
    protected HealthController m_healthController;

    protected FactionSO m_faction;
    protected UnitStats m_stats;


    private void Awake()
    {
        m_unitSelectionView = GetComponent<UnitSelectionView>();
        m_unitMovementController = GetComponent<UnitMovementController>();
        m_animationController = GetComponent<UnitAnimationController>();
        m_healthController = new HealthController(0, null);

        m_unitMovementController.OnStartMove.AddListener((arg1, arg2) => m_animationController.PlayMovingAnimation());
        m_unitMovementController.OnStopMove.AddListener((arg1) => m_animationController.PlayIdleAnimation());

        HandleAwake();
    }

    protected void MoveToLocation(Vector3 location)
    {
        m_unitMovementController.MoveTo(location);

        HandleMoveToLocation(location);
    }

    public void Setup(UnitStats stats, FactionSO faction)
    {
        m_stats = stats;
        m_faction = faction;

        m_healthController = new HealthController(stats.Health.Value, HandleHealthChanged);

        if (m_healthController != null)
            m_healthBarView.Setup(m_healthController);

        HandleSetup();
    }

    private void HandleHealthChanged(float currentHealth, float valueChanged)
    {
        if (m_healthController.IsAlive()) return;

        KillUnit();
    }


    public FactionSO GetFaction()
    {
        return m_faction;
    }

    public void Select()
    {
        m_unitSelectionView.Select();
    }
    public void Deselect()
    {
        m_unitSelectionView.Deselect();
    }

    public Transform GetPositon()
    {
        return transform;
    }

    public bool IsAlive()
    {
        return m_healthController.IsAlive();
    }

    private void KillUnit()
    {
        HandleKillUnit();

        m_unitMovementController.StopMovement(false);

        StartCoroutine(HandleKillUnitCoroutine());
    }

    private IEnumerator HandleKillUnitCoroutine()
    {
        yield return null;
        m_animationController.PlayDeathAnimation();

        Destroy(gameObject, m_timeToDestroySafety);
    }

    public void TakeDamage(ITargetableUnit source, float damage)
    {
        m_healthController.TakeDamage(damage);

        HandleTakeDamage(source, damage);
    }
    public bool IsActive()
    {
        return IsAlive();
    }

    protected virtual void HandleAwake() { }
    protected virtual void HandleTakeDamage(ITargetableUnit source, float damage) { }
    protected virtual void HandleMoveToLocation(Vector3 position) { }

    public abstract void HandleCommand(UnitCommand command);
    protected abstract void HandleKillUnit();
    protected abstract void HandleSetup();

}
