using System.Collections.Generic;
using Common;
using UI.Interfaces;
using UnityEngine.Assertions;

namespace Views
{
    public abstract class ViewsSwitching : BaseView
    {
        protected Dictionary<string, IGroupableSelection> selectionOptionsDictionary;
        protected override void Awake()
        {
            base.Awake();
            var barButtons = GetComponentsInChildren<IGroupableSelection>();
            var barButtonsNames = GetComponentsInChildren<INamedTempObject>();

            var buttonsCount = barButtons.Length;
            Assert.IsTrue(barButtons.Length == barButtonsNames.Length);

            selectionOptionsDictionary = new Dictionary<string, IGroupableSelection>(buttonsCount);
            for (int i = 0; i < buttonsCount; i++)
            {
                selectionOptionsDictionary.Add(barButtonsNames[i].Name, barButtons[i]); 
                barButtonsNames[i].Destroy();
            }
        }
    }
}