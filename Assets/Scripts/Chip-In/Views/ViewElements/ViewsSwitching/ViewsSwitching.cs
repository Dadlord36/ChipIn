using System.Collections.Generic;
using Common;
using Common.Interfaces;
using UnityEngine.Assertions;
using ViewModels.UI.Interfaces;

namespace Views.ViewElements.ViewsSwitching
{
    public abstract class ViewsSwitching : BaseView
    {
        protected Dictionary<string, IGroupAction> SelectionOptionsDictionary;
        protected override void Awake()
        {
            base.Awake();
            var barButtons = GetComponentsInChildren<IGroupAction>();
            var barButtonsNames = GetComponentsInChildren<INamedTempObject>();

            var buttonsCount = barButtons.Length;
            Assert.IsTrue(barButtons.Length == barButtonsNames.Length);

            SelectionOptionsDictionary = new Dictionary<string, IGroupAction>(buttonsCount);
            for (int i = 0; i < buttonsCount; i++)
            {
                SelectionOptionsDictionary.Add(barButtonsNames[i].Name, barButtons[i]); 
                barButtonsNames[i].Destroy();
            }
        }
    }
}