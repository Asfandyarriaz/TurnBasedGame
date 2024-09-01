using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    [SerializeField] private Unit _selectedUnit;
    [SerializeField] private LayerMask _unitLayerMask;
    private int _leftMouseButton = 0;

    public event EventHandler OnSelectedUnitChanged;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one UnitActionSystem! " + transform + " - " + Instance);
            Destroy(Instance);
            return;
        }
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            if (TryHandleUnitSelection())
                return;
            _selectedUnit.Move(MouseWorld.GetPosition());
        }
    }
    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _unitLayerMask))
        {
            if (hitInfo.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    void SelectedUnit(Unit unit)
    {
        _selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }
    public Unit GetSelectedUnit()
    {
        return _selectedUnit;
    }
}
