using System;

namespace pingak9
{
    public static class NativeDialog
    {
        public static void OpenDialog(string title, string message, string ok = "Ok", Action okAction = null)
        {
            MobileDialogInfo.Create(title, message, ok, okAction);
        }
        public static void OpenDialog(string title, string message, string yes, string no, Action yesAction = null, Action noAction = null)
        {
            MobileDialogConfirm.Create(title, message, yes, no, yesAction, noAction);
        }
        public static void OpenDialog(string title, string message, string accept, string neutral, string decline, Action acceptAction = null, Action neutralAction = null, Action declineAction = null)
        {
            MobileDialogNeutral.Create(title, message, accept, neutral, decline, acceptAction, neutralAction, declineAction);
        }
    }
}