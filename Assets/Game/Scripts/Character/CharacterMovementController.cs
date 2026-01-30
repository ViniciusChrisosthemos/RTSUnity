using System;
using System.Collections;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _root;

    [Header("Parameters")]
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _turnSpeed = 500f;

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _runningParameterName = "Running";

    private Coroutine _movementAnimationCoroutine;

    public void MoveTo(Vector3 targetPosition, float distanceThreshold=0.1f, Action callback=null)
    {
        if (_movementAnimationCoroutine != null)
        {
            StopCoroutine(_movementAnimationCoroutine);
        }

        _movementAnimationCoroutine = StartCoroutine(AnimateMovementCoroutine(targetPosition, distanceThreshold, callback));
    }


    public IEnumerator AnimateMovementCoroutine(Vector3 targetPosition, float distanceThreshold, Action callback)
    {
        var distance = float.MaxValue;
        
        var targetDirection = (targetPosition - _root.position).normalized;
        var targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        StartMovementAnimation();

        do
        {
            if (Quaternion.Angle(_root.rotation, targetRotation) > 0.1f)
            {
                _root.rotation = Quaternion.RotateTowards(_root.rotation, targetRotation, _turnSpeed * Time.deltaTime);
            }

            _root.position = Vector3.MoveTowards(_root.position, targetPosition, _moveSpeed * Time.deltaTime);

            distance = Vector3.Distance(_root.position, targetPosition);

            yield return null;
        } while (distance > distanceThreshold);

        //_root.position = targetPosition;

        StopMovementAnimation();

        callback?.Invoke();
    }

    private void StartMovementAnimation()
    {
        _animator.SetBool(_runningParameterName, true);
    }

    private void StopMovementAnimation()
    {
        _animator.SetBool(_runningParameterName, false);
    }
}
