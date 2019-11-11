using UI.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Elements
{
    public class BarButton : Button, IGroupableSelection
    {
        [SerializeField] private GameObject bottomBarCovering;
        private IViewable _bottomBarCoveringViewable;

        protected override void Awake()
        {
            base.Awake();
            Assert.IsTrue(bottomBarCovering.TryGetComponent(out _bottomBarCoveringViewable));
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
            _bottomBarCoveringViewable.Hide();
        }

        void IGroupableSelection.SelectAsOneOfGroup()
        {
            DoStateTransition(SelectionState.Selected, false);
            _bottomBarCoveringViewable.Show();
        }

        void IGroupableSelection.SubscribeOnMainEvent(IGroupableSelection groupableSelection)
        {
            onClick.AddListener(groupableSelection.OnOtherItemSelected);
        }
    }
}