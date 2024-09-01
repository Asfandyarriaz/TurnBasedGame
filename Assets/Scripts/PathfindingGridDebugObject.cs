using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PathfindingGridDebugObject : GridDebugObject
{
    [SerializeField] private TextMeshProUGUI _gCostText;
    [SerializeField] private TextMeshProUGUI _hCostText;
    [SerializeField] private TextMeshProUGUI _fCostText;
    [SerializeField] private SpriteRenderer _isWalkableSpriteRenderer;

    private PathNode _pathNode;
    public override void SetGridObject(object gridObject)
    {
        base.SetGridObject(gridObject);
        _pathNode = (PathNode)gridObject;
    }

    protected override void Update()
    {
        base.Update();
        _gCostText.text = _pathNode.GetGCost().ToString();
        _hCostText.text = _pathNode.GetHCost().ToString();
        _fCostText.text = _pathNode.GetFCost().ToString();
        _isWalkableSpriteRenderer.color = _pathNode.IsWalkable() ? Color.green : Color.red;
    }

}
