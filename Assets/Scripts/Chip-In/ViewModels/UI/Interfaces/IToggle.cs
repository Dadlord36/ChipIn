namespace ViewModels.UI.Interfaces
{
    public interface IToggle
    {
        bool Condition { get; }
        void SetCondition(bool newCondition, bool notifyConditionChanged);
        void SwitchCondition(bool notifyConditionChanged);
    }
}