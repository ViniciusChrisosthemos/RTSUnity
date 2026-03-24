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

    [Header("Events")]
    public UnityEvent<UnitMovementController, Vector3> OnStartMove; 
    public UnityEvent<UnitMovementController> OnStopMove; 

    private Coroutine m_movementAnimationCoroutine;

    public void MoveTo(Vector3 targetPosition, float distanceThreshold=0.1f, Action callback=null)
    {
        if (m_movementAnimationCoroutine != null)
        {
            StopCoroutine(m_movementAnimationCoroutine);
        }

        m_movementAnimationCoroutine = StartCoroutine(AnimateMovementCoroutine(targetPosition, distanceThreshold, callback));
    }


    public IEnumerator AnimateMovementCoroutine(Vector3 targetPosition, float distanceThreshold, Action callback)
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

        //_root.position = targetPosition;

        OnStopMove?.Invoke(this);

        callback?.Invoke();
    }

    public void SetSpeed(float speed)
    {
        m_moveSpeed = speed;
    }
}
