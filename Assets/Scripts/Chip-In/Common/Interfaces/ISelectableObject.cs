using System;

namespace Common.Interfaces
{
    public interface ISelectableObject
    {
        event Action Selected;
        event Action Deselected;
    }

    public interface INotifySelectionWithIdentifier : IIndex
    {
        event Action<INotifySelectionWithIdentifier> Selected;
        bool IsSelected { get; set; }
        void SetInitialState(bool state);
    }
}