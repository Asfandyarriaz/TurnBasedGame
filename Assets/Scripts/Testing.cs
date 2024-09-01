using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private GridSystemVisual _gridSystemVisual;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _unit.GetMoveAction().GetValidActionGridPositionList();
            GridSystemVisual.Instance.HideAllGridPosition();
            GridSystemVisual.Instance.ShowGridPositionList(_unit.GetMoveAction().GetValidActionGridPositionList());
        }
    }
}
