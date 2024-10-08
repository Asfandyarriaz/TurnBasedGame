using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] TrailRenderer _trailRenderer;
    [SerializeField] private Transform _bulletHitVfxPrefab;

    private Vector3 _targetPosition;
    private float moveSpeed = 200f;
    public void Setup(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    private void Update()
    {
        Vector3 moveDir = (_targetPosition - transform.position).normalized;

        float distanceBeforeMoving = Vector3.Distance(transform.position, _targetPosition);

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float distanceAfterMoving = Vector3.Distance(transform.position, _targetPosition);

        if(distanceBeforeMoving < distanceAfterMoving)
        {

            transform.position = _targetPosition;

            _trailRenderer.transform.parent = null;

            Destroy(gameObject);

            Instantiate(_bulletHitVfxPrefab, transform.position, Quaternion.identity);
        }

    }
}
