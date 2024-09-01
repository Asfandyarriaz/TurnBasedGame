using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] Unit _selectedUnit;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UpdateVisuals();
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty)
    {
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        if (UnitActionSystem.Instance.GetSelectedUnit() == _selectedUnit)
            _meshRenderer.enabled = true;
        else
            _meshRenderer.enabled = false;
    }
}
