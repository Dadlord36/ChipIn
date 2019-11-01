using ScriptableObjects.Parameters;
using UnityEngine;

namespace UI.Effects.UIGradient
{
    [AddComponentMenu("UI/Effects/ParametricGradient")]
    class UIGradientParametric : UIGradient
    {
        [SerializeField] private LinearGradientParameter linearGradientParameter;

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if (!linearGradientParameter) return;

            m_color1 = linearGradientParameter.Color1;
            m_color2 = linearGradientParameter.Color2;
            m_angle = linearGradientParameter.angle;
            m_ignoreRatio = linearGradientParameter.ignoreRatio;
        }
#endif
    }
}