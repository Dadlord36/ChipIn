using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Elements
{
    public interface IGroupableSelection
    {
        void OnOtherItemSelected();
        void SelectAsOneOfGroup();

        void SubscribeOnMainEvent(IGroupableSelection groupableSelection);
    }

    public class BarButton : Button,IGroupableSelection
    {
        [SerializeField] private BarButtonSelection barButtonSelection;

        protected override void Start()
        {
            base.Start();
            Assert.IsNotNull(barButtonSelection);
        }

        public override void OnSelect(BaseEventData eventData)
        {
        }

        public override void OnDeselect(BaseEventData eventData)
        {
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            ((IGroupableSelection) this).SelectAsOneOfGroup();
        }

        void IGroupableSelection.OnOtherItemSelected()
        {
            DoStateTransition(SelectionState.Normal, false);
            barButtonSelection.Hide();
        }

        void IGroupableSelection.SelectAsOneOfGroup()
        {
            DoStateTransition(SelectionState.Selected, false);
            barButtonSelection.Show();
        }

        void IGroupableSelection.SubscribeOnMainEvent(IGroupableSelection groupableSelection)
        {
            onClick.AddListener(groupableSelection.OnOtherItemSelected);
        }
    }
}