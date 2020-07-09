namespace ScriptableObjects.SwitchBindings
{
    public readonly struct ViewsPairInfo
    {
        public readonly string ViewToSwitchFromName;
        public readonly string ViewToSwitchToName;

        public ViewsPairInfo(string viewToSwitchFromName, string viewToSwitchToName)
        {
            ViewToSwitchFromName = viewToSwitchFromName;
            ViewToSwitchToName = viewToSwitchToName;
        }
    }
}