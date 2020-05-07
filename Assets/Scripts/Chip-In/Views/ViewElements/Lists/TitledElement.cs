using System;
using ScriptableObjects.Parameters;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ViewModels.UI.Interfaces;
using Views.Bars.BarItems;

namespace Views.ViewElements.Lists
{
    [RequireComponent(typeof(Button))]
    public sealed class TitledElement : BaseView, ITitled, IOneOfAGroup
    {
        [SerializeField] private TMP_Text titleTextField;

        public event Action<string> WasSelected; 
        public event UnityAction GroupActionPerformed;
        [SerializeField] private Image background;
        [SerializeField] private ColorsPairParameter backgroundColoursPair;
        
        
        public Color BackgroundColor
        {
            get => background.color;
            set => background.color = value;
        }

        public string Title
        {
            get => titleTextField.text;
            set => titleTextField.text = value;
        }

        private Button RelatedButton => GetComponent<Button>();

        protected override void OnEnable()
        {
            base.OnEnable();
            RelatedButton.onClick.AddListener(OnButtonClicked);
            SwitchToUnselectedStyle();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            RelatedButton.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            OnWasSelected(Title);
            SwitchToSelectedStyle();
            OnGroupActionPerformed();
        }

        private void OnWasSelected(string obj)
        {
            WasSelected?.Invoke(obj);
        }

        private void SwitchToSelectedStyle()
        {
            BackgroundColor = backgroundColoursPair.value1;
        }

        private void SwitchToUnselectedStyle()
        {
            BackgroundColor = backgroundColoursPair.value2;
        }

        public void OnOtherOnePerformGroupAction()
        {
            SwitchToUnselectedStyle();
        }


        private void OnGroupActionPerformed()
        {
            GroupActionPerformed?.Invoke();
        }
    }
}