using UnityEngine;

namespace Controllers.SlotsSpinningControllers
{
    [RequireComponent(typeof(LineEngineBehaviour))]
    public class LineEngineRowController : LineEngineController
    {
        public void Prepare(uint targetIndex)
        {
            SetLineEngine();
            LineEngineBehaviour.IndexOfItemToFocusOn = targetIndex;
            LineEngineBehaviour.Initialize();
        }
    }
}