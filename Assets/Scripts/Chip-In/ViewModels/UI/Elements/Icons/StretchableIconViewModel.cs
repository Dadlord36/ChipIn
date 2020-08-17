using Controllers;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace ViewModels.UI.Elements.Icons
{
    [Binding]
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