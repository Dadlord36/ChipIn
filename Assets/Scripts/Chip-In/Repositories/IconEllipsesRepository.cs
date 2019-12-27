using System;
using UI.Elements.Icons;
using UnityEngine;

namespace Repositories
{
    [Serializable]
    public struct IconEllipseData
    {
        public Sprite sprite;
        public float scale;
    }
    
    
    [CreateAssetMenu(fileName = nameof(IconEllipsesRepository),
        menuName = nameof(Repositories) + "/" + nameof(IconEllipsesRepository), order = 0)]
    public class IconEllipsesRepository : ScriptableObject
    {
        [SerializeField] private IconEllipseData silverEllipse, goldenEllipse;

        public IconEllipseData GetEllipse(IconEllipseType ellipseType)
        {
            switch (ellipseType)
            {
                case IconEllipseType.Golden:
                    return goldenEllipse;
                case IconEllipseType.Silver:
                    return silverEllipse;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ellipseType), ellipseType, null);
            }
        }
    }
}