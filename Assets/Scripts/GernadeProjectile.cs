using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GernadeProjectile : MonoBehaviour
{
    public static event EventHandler OnAnyGernadeExploded;

    [SerializeField] private Transform _gernadeExplodeVfxPrefab;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private AnimationCurve _arcYAnimationCurve;

    private Vector3 _targetPosition;
    private Action _onGernadeBehaviourComplete;
    private float _moveSpeed = 15f;
    private float _reachedTargetDistance = 0.2f;
    private float _damageRaduis = 4f;
    private float _totalDistance;
    private Vector3 _positionXZ;

    private void Update()
    {
        Vector3 movDir = (_targetPosition - _positionXZ).normalized;
        _positionXZ += movDir * _moveSpeed * Time.deltaTime;


        float distance = Vector3.Distance(_positionXZ, _targetPosition);
        float distanceNormalized = 1 - distance / _totalDistance;

        float maxHeight = _totalDistance / 4f;
        float positionY = _arcYAnimationCurve.Evaluate(distanceNormalized) * maxHeight;
        transform.position = new Vector3(_positionXZ.x, positionY, _positionXZ.z);


        if (Vector3.Distance(_positionXZ, _targetPosition) < _reachedTargetDistance)
        {
            Collider[] colliderArray = Physics.OverlapSphere(_targetPosition, _damageRaduis);

            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent<Unit>(out Unit targetUnit))
                {
                    targetUnit.Damage(30);
                }

                if (collider.TryGetComponent<DestructibleCrate>(out DestructibleCrate destructibleCrate))
                {
                    destructibleCrate.Damage();
                }
            }

            OnAnyGernadeExploded?.Invoke(this, EventArgs.Empty);
            _trailRenderer.transform.parent = null;

            Instantiate(_gernadeExplodeVfxPrefab, _targetPosition + Vector3.up * 1, Quaternion.identity);

            Destroy(gameObject);

            _onGernadeBehaviourComplete();
        }
    }


    public void Setup(GridPosition targetGridPosition, Action onGernadeBehaviourComplete)
    {
        _onGernadeBehaviourComplete = onGernadeBehaviourComplete;
        _targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);


        _positionXZ = transform.position;
        _positionXZ.y = 0;
        _totalDistance = Vector3.Distance(_positionXZ, _targetPosition);
    }
}
