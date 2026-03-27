using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UnitMovementController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform m_root;

    [Header("Parameters")]
    [SerializeField] private float m_moveSpeed = 4f;
    [SerializeField] private float m_turnSpeed = 500f;
    [SerializeField] private float m_lookToTargetDuration = 0.2f;

    [Header("Events")]
    public UnityEvent<UnitMovementController, Vector3> OnStartMove; 
    public UnityEvent<UnitMovementController> OnStopMove; 

    private Coroutine m_movementAnimationCoroutine;

    public void MoveTo(Vector3 targetPosition, float distanceThreshold=0.1f, Action callback=null, bool notify=true)
    {
        StopMovement(false);

        m_movementAnimationCoroutine = StartCoroutine(AnimateMovementCoroutine(targetPosition, distanceThreshold, callback, notify));
    }


    public IEnumerator AnimateMovementCoroutine(Vector3 targetPosition, float distanceThreshold, Action callback, bool notify = true)
    {
        var distance = float.MaxValue;
        
        var targetDirection = (targetPosition - m_root.position).normalized;
        var targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        OnStartMove?.Invoke(this, targetPosition);

        do
        {
            if (Quaternion.Angle(m_root.rotation, targetRotation) > 0.1f)
            {
                m_root.rotation = Quaternion.RotateTowards(m_root.rotation, targetRotation, m_turnSpeed * Time.deltaTime);
            }

            m_root.position = Vector3.MoveTowards(m_root.position, targetPosition, m_moveSpeed * Time.deltaTime);

            distance = Vector3.Distance(m_root.position, targetPosition);

            yield return null;

        } while (distance > distanceThreshold);

        callback?.Invoke();

        StopMovement(notify);
    }

    public IEnumerator LookToTargetCoroutine(Transform root, Vector3 target, float duration)
    {
        var accumTime = 0f;

        var startRotation = root.rotation;
        var endRotation = Quaternion.LookRotation(target - root.position, Vector3.up);

        while (accumTime < duration)
        {
            accumTime += Time.deltaTime;

            root.rotation = Quaternion.Lerp(startRotation, endRotation, accumTime / duration);

            yield return null;
        }
    }

    public void SetSpeed(float speed)
    {
        m_moveSpeed = speed;
    }

    public void LookToTarget(Vector3 target)
    {
        StartCoroutine(LookToTargetCoroutine(transform, target, m_lookToTargetDuration));
    }

    public void StopMovement(bool notify)
    {
        if (m_movementAnimationCoroutine != null)
        {
            StopCoroutine(m_movementAnimationCoroutine);
        }

        if (notify) OnStopMove?.Invoke(this);
    }
}
