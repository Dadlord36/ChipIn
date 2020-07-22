using Common.UnityEvents;
using UnityWeld.Binding;

[Binding]
public sealed class TwoButtonsOptionSelectorWithStringOptions : TwoButtonsOptionSelector
{
    public StringUnityEvent selectedOptionChanged;
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
            OnSelectedOptionChanged(value);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _selectableStringOptions = GetComponent<SelectableStringOptions>();
        SelectDefault();
    }

    public void SetOptionByIndex(int index)
    {
        SelectedOption = _selectableStringOptions[index];
    }

    private void SelectDefault()
    {
        SelectedOption = _selectableStringOptions[0];
    }

    private void OnSelectedOptionChanged(in string value)
    {
        selectedOptionChanged.Invoke(value);
    }
}