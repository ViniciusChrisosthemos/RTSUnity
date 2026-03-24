using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator m_animator;

    [Header("Animations Triggers")]
    [SerializeField] private string m_triggerMoving;
    [SerializeField] private string m_triggerlAttacking;
    [SerializeField] private string m_triggerIdle;

    public void PlayIdleAnimation()
    {
        m_animator.SetTrigger(m_triggerIdle);
    }

    public void PlayMovingAnimation()
    {
        m_animator.SetTrigger(m_triggerMoving);
    }

    public void PlayAttackingAnimation()
    {
        m_animator.SetTrigger(m_triggerIdle);
    }
}
