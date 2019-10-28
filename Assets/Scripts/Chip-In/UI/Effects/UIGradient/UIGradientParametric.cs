﻿using ScriptableObjects.Parameters;
using UnityEngine;

namespace UI.Effects.UIGradient
{
    [AddComponentMenu("UI/Effects/ParametricGradient")]
    class UIGradientParametric : UIGradient
    {
        [SerializeField] private LinearGradientParameter linearGradientParameter;

        protected override void OnValidate()
        {
            base.OnValidate();
            if(!linearGradientParameter) return;
            
            m_color1 = linearGradientParameter.color1;
            m_color2 = linearGradientParameter.color2;
            m_angle = linearGradientParameter.angle;
            m_ignoreRatio = linearGradientParameter.ignoreRatio;
        }
    }
}