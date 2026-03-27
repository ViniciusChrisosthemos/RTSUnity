using UnityEngine;
using UnityEngine.Events;

public class AnimationTriggersHelper : MonoBehaviour
{
    public UnityEvent OnAttackHitTarget;

    public void TriggerAttackHitTarget()
    {
        OnAttackHitTarget?.Invoke();
    }
}
