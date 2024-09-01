using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float _totalSpinAddAmount;
    public Action OnActionComplete;
    private void Update()
    {
        if (!_isActive)
            return;

        float spinAddAmount = 360f * Time.deltaTime;
        _totalSpinAddAmount += spinAddAmount;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        if (_totalSpinAddAmount >= 360)
        {
            ActionComplete();
        }

    }

    public override void TakeAction(GridPosition gridPosition, Action OnActionComplete)
    {
        _totalSpinAddAmount = 0;

        ActionStart(OnActionComplete);
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = _unit.GetGridPosition();

        return new List<GridPosition>
        {
            unitGridPosition,
        };
    }

    public override int GetActionPointCost()
    {
        return 1;
    }


    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 0,
        };
    }


}
