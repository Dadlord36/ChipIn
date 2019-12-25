using System.Collections.Generic;
using Common;
using UI.Interfaces;
using UnityEngine.Assertions;

namespace Views.ViewElements.ViewsSwitching
{
    public abstract class ViewsSwitching : BaseView
    {
        protected Dictionary<string, IGroupableSelection> SelectionOptionsDictionary;
        protected override void Awake()
        {
            base.Awake();
            var barButtons = GetComponentsInChildren<IGroupableSelection>();
            var barButtonsNames = GetComponentsInChildren<INamedTempObject>();

            var buttonsCount = barButtons.Length;
            Assert.IsTrue(barButtons.Length == barButtonsNames.Length);

            SelectionOptionsDictionary = new Dictionary<string, IGroupableSelection>(buttonsCount);
            for (int i = 0; i < buttonsCount; i++)
            {
                SelectionOptionsDictionary.Add(barButtonsNames[i].Name, barButtons[i]); 
                barButtonsNames[i].Destroy();
            }
        }
    }
}