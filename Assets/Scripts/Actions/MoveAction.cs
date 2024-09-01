using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] Animator _unitAnimator;
    private float _speed = 4f;
    private Vector3 _targetPosition;
    private float _stoppingDistance = 0.1f;
    private float _rotateSpeed = 10f;
    [SerializeField] private int _maxMoveDistance;
    public Action MoveCompleteAction;


    protected override void Awake()
    {
        base.Awake();
        _targetPosition = transform.position;
    }
    private void Update()
    {
        if (!_isActive)
            return;

        Vector3 moveDirection = (_targetPosition - transform.position).normalized;
        if (Vector3.Distance(transform.position, _targetPosition) >= _stoppingDistance)
        {
            transform.position += moveDirection * _speed * Time.deltaTime;

            _unitAnimator.SetBool("isWalking", true);
        }
        else
        {
            _unitAnimator.SetBool("isWalking", false);
            _isActive = false;
            MoveCompleteAction();
        }

        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * _rotateSpeed);
    }

    public override void TakeAction(GridPosition gridPosition, Action MoveCompleteAction)
    {
        _isActive = true;
        _targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        this.MoveCompleteAction = MoveCompleteAction;
    }
    
    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = _unit.GetGridPosition();
        for (int x = -_maxMoveDistance; x <= _maxMoveDistance; x++)
        {
            for (int z = -_maxMoveDistance; z <= _maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                if (unitGridPosition == testGridPosition)
                {
                    //Same grid position where the unit is already at
                    continue;
                }
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    //Grid position already occupied with another unit
                    continue;
                }


                validGridPositionList.Add(testGridPosition);
                Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }
}
