using System;

namespace ViewModels.Helpers
{
    public interface IViewsSwitchingHelper
    {
        event Action<string> SwitchedToView; 
        void SwitchToView(in string typeOfView);
    }
}