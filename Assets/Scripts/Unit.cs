using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] Animator _unitAnimator;
    private float _speed = 4f;
    private Vector3 _targetPosition;
    private float _stoppingDistance = 0.1f;
    private float _rotateSpeed = 10f;

    private void Awake()
    {
        _targetPosition = transform.position;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, _targetPosition) >= _stoppingDistance)
        {
            Vector3 moveDirection = (_targetPosition - transform.position).normalized;
            transform.position += moveDirection * _speed * Time.deltaTime;

            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * _rotateSpeed);
            _unitAnimator.SetBool("isWalking", true);
        }
        else
            _unitAnimator.SetBool("isWalking", false);

    }

    public void Move(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}
