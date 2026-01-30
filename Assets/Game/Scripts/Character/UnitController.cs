using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider), typeof(Outline))]
public class UnitController : MonoBehaviour, ITarget, IPointerClickHandler
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private float _attackDamage = 10f;
    [SerializeField] private float _attackSpeed = 1.5f;

    [SerializeField] private CharacterMovementController _movementController;

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _attackTriggerName = "Attack";
    [SerializeField] private string _takeDamageTriggerName = "TakeDamage";
    [SerializeField] private string _killTriggerName = "Kill";

    private ITarget _currentTarget;
    private Coroutine _attackCoroutine;

    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    private void Start()
    {
        _outline.DisableOutline();
    }

    public void AttackTarget(ITarget target)
    {
        _currentTarget = target;

        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }

        _attackCoroutine = StartCoroutine(AttackTargetCoroutine());
    }

    private IEnumerator AttackTargetCoroutine()
    {
        var targetPosition = _currentTarget.GetTransform().position;

        yield return _movementController.AnimateMovementCoroutine(targetPosition, _attackRange, null);

        while (_currentTarget.IsAlive())
        {
            Attack(_currentTarget);

            yield return new WaitForSeconds(_attackSpeed);
        }
    }

    private async void Attack(ITarget target)
    {
        _animator.SetTrigger(_attackTriggerName);

        await System.Threading.Tasks.Task.Delay(500);
        
        target.TakeDamage((int)_attackDamage);
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public bool IsAlive()
    {
        return _health > 0;
    }

    public void KillTarget()
    {
        _animator.SetTrigger(_killTriggerName);

        GetComponent<Collider>().enabled = false;
    }

    public void TakeDamage(int damage)
    {
        _animator.SetTrigger(_takeDamageTriggerName);

        _health -= damage;

        if (_health <= 0)
        {
            KillTarget();
        }
    }

    public void MoveTo(Vector3 targetPosition)
    {
        _movementController.MoveTo(targetPosition);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsAlive()) return;

        Debug.Log("On Pointer Click");
        UnitManager.Instance.SelectUnit(this);
    }

    public void SelectTarget()
    {
        _outline.EnableOutline();
    }

    public void DeselectTarget()
    {
        _outline.DisableOutline();
    }
}
