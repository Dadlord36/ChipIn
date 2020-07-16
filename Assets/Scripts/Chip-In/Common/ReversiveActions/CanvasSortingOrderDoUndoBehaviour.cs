using UnityEngine;

namespace Common.ReversiveActions
{
    public class CanvasSortingOrderDoUndoBehaviour : DoUndoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private int newSortingOrderValue;
            
        private bool _originalOverrideSortingOrderState;
        private int _originalSortingOrder;

        public override void Do()
        {
            RecordValues();
            ApplyNewValues();
        }

        public override void Undo()
        {
            RestoreValues();
        }

        private void ApplyNewValues()
        {
            canvas.overrideSorting = true;
            canvas.sortingOrder = newSortingOrderValue;
        }
        
        private void RecordValues()
        {
            _originalOverrideSortingOrderState = canvas.overrideSorting;
            _originalSortingOrder = canvas.sortingOrder;
        }

        private void RestoreValues()
        {
            canvas.overrideSorting = _originalOverrideSortingOrderState;
            canvas.sortingOrder = _originalSortingOrder;
        }
    }
}