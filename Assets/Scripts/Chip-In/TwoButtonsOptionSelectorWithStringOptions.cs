using Common.UnityEvents;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class TwoButtonsOptionSelectorWithStringOptions : TwoButtonsOptionSelector
{
    [SerializeField] private StringUnityEvent selectionChanged;
    private SelectableStringOptions _selectableStringOptions;
    private string _selectedOption;


    [Binding]
    public string SelectedOption
    {
        get => _selectedOption;
        set
        {
            if (value == _selectedOption) return;
            _selectedOption = value;
            OnSelectionChanged(value);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _selectableStringOptions = GetComponent<SelectableStringOptions>();
        SelectDefault();
    }

    private void SelectDefault()
    {
        SelectedOption = _selectableStringOptions[0];
    }

    private void OnSelectionChanged(string obj)
    {
        selectionChanged?.Invoke(obj);
    }
}