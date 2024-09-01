using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    private GridObject _gridObject;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    public void SetGridObject(GridObject gridObject)
    {
        _gridObject = gridObject;
    }
    private void Update()
    {
        _textMeshProUGUI.text = _gridObject.ToString();
    }

}
