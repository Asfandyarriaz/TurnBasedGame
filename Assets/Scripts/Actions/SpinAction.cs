using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{

    private float _totalSpinAddAmount;
    public Action SpinCompleteAction;
    private void Update()
    {
        if (!_isActive)
            return;

        float spinAddAmount = 360f * Time.deltaTime;
        _totalSpinAddAmount += spinAddAmount;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        if (_totalSpinAddAmount >= 360)
        {
            _isActive = false;
            SpinCompleteAction();
        }

    }

    public override void TakeAction(GridPosition gridPosition, Action SpinCompleteAction)
    {
        _isActive = true;
        _totalSpinAddAmount = 0;
        this.SpinCompleteAction = SpinCompleteAction;
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
}
