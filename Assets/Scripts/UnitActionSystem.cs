using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    [SerializeField] private Unit _selectedUnit;
    [SerializeField] private LayerMask _unitLayerMask;

    public event EventHandler OnSelectedUnitChanged;
    private bool _isBusy;
    private BaseAction _selectedAction;

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
    private void Start()
    {
        SetSelectedUnit(_selectedUnit);

    }
    private void Update()
    {
        if (_isBusy)
        {
            return;
        }
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (TryHandleUnitSelection())
        {
            return;
        }

        HandleSelectedAction();
    }
    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (_selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                SetBusy();
                _selectedAction.TakeAction(mouseGridPosition, ClearBusy);
            }
        }
    }
    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _unitLayerMask))
            {
                if (hitInfo.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    if(unit == _selectedUnit)
                    {
                        // Unit is already selected
                        return false;
                    }
                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }
        return false;
    }

    void SetSelectedUnit(Unit unit)
    {
        _selectedUnit = unit;
        SetSelectedAction(unit.GetMoveAction());

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }
    public void SetSelectedAction(BaseAction baseAction)
    {
        _selectedAction = baseAction;
    }
    public Unit GetSelectedUnit()
    {
        return _selectedUnit;
    }
    public BaseAction GetSelectedAction()
    {
        return _selectedAction;
    }    
    private void SetBusy()
    {
        _isBusy = true;
    }
    private void ClearBusy()
    {
        _isBusy = false;
    }
}
