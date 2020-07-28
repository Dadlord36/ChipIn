using UnityEngine;
using ViewModels.UI.Elements.Buttons;

public class TwoButtonsOptionSelector : MonoBehaviour
{
    [SerializeField] private GroupedHighlightedButton selectedByDefaultButton;


    protected virtual void Start()
    {
        selectedByDefaultButton.PerformGroupAction();
    }
}