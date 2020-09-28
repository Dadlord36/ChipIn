using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace ViewModels.UI.Elements.Icons
{
    [RequireComponent(typeof(IconSizeFitterController))]
    public sealed class StretchableIconViewModel : Image
    {
       public override void SetAllDirty()
        {
            GetComponent<IconSizeFitterController>().FitImage();
            base.SetAllDirty();
        }
    }
}