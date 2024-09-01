using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _currentTurnText;
    [SerializeField] Button _endTurnBtn;
    [SerializeField] GameObject _enemyTurnVisualGameObject;


    private void Start()
    {
        _endTurnBtn.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
        });

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        UpdateTurnNumber();
        UpdateEnemyTurnVisual();
        UpdateEndTurnBtnVisibility();
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateTurnNumber();
        UpdateEnemyTurnVisual();
        UpdateEndTurnBtnVisibility();
    }

    private void UpdateTurnNumber()
    {
        _currentTurnText.text = "TURN " + TurnSystem.Instance.GetTurnNumber();
    }

    private void UpdateEnemyTurnVisual()
    {
        _enemyTurnVisualGameObject.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }

    private void UpdateEndTurnBtnVisibility()
    {
        _endTurnBtn.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }
}
