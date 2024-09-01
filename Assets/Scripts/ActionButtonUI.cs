using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _selectedGameObject;
    private BaseAction _baseAction;

    public void SetBaseAction(BaseAction baseAction)
    {
        _text.text = baseAction.GetActionName().ToUpper();
        _baseAction = baseAction;

        _button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
        });
    }

    public void UpdateSelectedVisual()
    {
        BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
        _selectedGameObject.SetActive(selectedBaseAction == _baseAction);
    }
}
