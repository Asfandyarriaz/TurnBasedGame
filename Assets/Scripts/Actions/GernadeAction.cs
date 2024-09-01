using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GernadeAction : BaseAction
{
    [SerializeField] private Transform _gernadeProjectilePrefab;
    private int _maxThrowDistance = 7;

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }

    }


    public override string GetActionName()
    {
        return "Gernade";
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 0,
        };
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = _unit.GetGridPosition();

        for (int x = -_maxThrowDistance; x <= _maxThrowDistance; x++)
        {
            for (int z = -_maxThrowDistance; z <= _maxThrowDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);

                if (testDistance > _maxThrowDistance)
                {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        Transform gernadeProjectileTransform = Instantiate(_gernadeProjectilePrefab, _unit.GetWorldPosition(), Quaternion.identity);
        GernadeProjectile gernadeProjectle = gernadeProjectileTransform.GetComponent<GernadeProjectile>();
        gernadeProjectle.Setup(gridPosition, OnGernadeBehaviourComplete);
        Debug.Log("Gernade Action");

        ActionStart(onActionComplete);
    }

    private void OnGernadeBehaviourComplete()
    {
        ActionComplete();
    }
}
