using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _button;

    public void SetBaseAction(BaseAction baseAction)
    {
        _text.text = baseAction.GetActionName().ToUpper();

        _button.onClick.AddListener(() => { 
        UnitActionSystem.Instance.SetSelectedAction(baseAction);
        });
    }
}
