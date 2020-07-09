using System.Linq;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace Views.ViewElements.ViewsSwitching
{
    public class GradientRadarLine : UILineTextureRenderer
    {
        [SerializeField] private DotOnCircle[] dotsOnCircles;

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            var circlesNum = dotsOnCircles.Length;
            base.OnValidate();
            Points = new Vector2[circlesNum+1];

            for (int i = 0; i < circlesNum; i++)
            {
                Points[i] = dotsOnCircles[i].DotPosition;
            }
            Points[circlesNum] = dotsOnCircles.First().DotPosition;
        }
#endif
    }
}