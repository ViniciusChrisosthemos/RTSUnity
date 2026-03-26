using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnitAnimationController : MonoBehaviour
{
    [Header("Animations Triggers")]
    [SerializeField] private string m_triggerMoving;
    [SerializeField] private string m_triggerlAttacking;
    [SerializeField] private string m_triggerIdle;

    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void PlayIdleAnimation()
    {
        SetAllBoolsToFalse();
        m_animator.SetBool(m_triggerIdle, true);

    }

    public void PlayMovingAnimation()
    {
        SetAllBoolsToFalse();
        m_animator.SetBool(m_triggerMoving, true);
    }

    public void PlayAttackingAnimation()
    {
        SetAllBoolsToFalse();
        m_animator.SetBool(m_triggerIdle, true);
    }

    public void SetAllBoolsToFalse()
    {
        foreach(var paramter in m_animator.parameters)
        {
            if (paramter.type == AnimatorControllerParameterType.Bool)
            {
                m_animator.SetBool(paramter.name, false);
            }
        }
    }
}
