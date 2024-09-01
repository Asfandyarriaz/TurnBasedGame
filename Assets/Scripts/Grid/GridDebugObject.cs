using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private object _gridObject;

    public virtual void SetGridObject(object gridObject)
    {
        _gridObject = gridObject;
    }
    protected virtual void Update()
    {
        _textMeshProUGUI.text = _gridObject.ToString();
    }

}
