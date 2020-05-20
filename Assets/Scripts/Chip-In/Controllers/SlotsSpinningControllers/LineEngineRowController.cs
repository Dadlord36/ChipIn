using UnityEngine;

namespace Controllers.SlotsSpinningControllers
{
    [RequireComponent(typeof(LineEngine))]
    public class LineEngineRowController : LineEngineController
    {
        public void Prepare(uint targetIndex)
        {
            SetLineEngine();
            LineEngine.ItemToFocusOnIndex = targetIndex;
            LineEngine.Initialize();
        }
    }
}