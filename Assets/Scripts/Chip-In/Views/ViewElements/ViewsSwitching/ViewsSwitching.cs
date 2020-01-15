using System.Collections.Generic;
using Common;
using UnityEngine.Assertions;
using ViewModels.UI.Interfaces;

namespace Views.ViewElements.ViewsSwitching
{
    public abstract class ViewsSwitching : BaseView
    {
        protected Dictionary<string, ISelectableObject> SelectionOptionsDictionary;
        protected override void Awake()
        {
            base.Awake();
            var barButtons = GetComponentsInChildren<ISelectableObject>();
            var barButtonsNames = GetComponentsInChildren<INamedTempObject>();

            var buttonsCount = barButtons.Length;
            Assert.IsTrue(barButtons.Length == barButtonsNames.Length);

            SelectionOptionsDictionary = new Dictionary<string, ISelectableObject>(buttonsCount);
            for (int i = 0; i < buttonsCount; i++)
            {
                SelectionOptionsDictionary.Add(barButtonsNames[i].Name, barButtons[i]); 
                barButtonsNames[i].Destroy();
            }
        }
    }
}