using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Views;

namespace ScriptableObjects.Comparators
{
    [CreateAssetMenu(fileName = nameof(ViewsComparisonContainer),
        menuName = nameof(Comparators) + "/" + nameof(ViewsComparisonContainer),
        order = 0)]
    public class ViewsComparisonContainer : ScriptableObject
    {
        [SerializeField] private List<BaseView> views;

        public bool ContainsView(BaseView view)
        {
            return views.Exists(baseView => baseView.ViewName == view.ViewName);
        }

        public enum RelativePositionInArray
        {
            Before,
            After
        }

        public RelativePositionInArray GetRelativePositionInContainer(string thisViewName, string otherViewName)
        {
            var thisViewIndex = views.FindIndex(view => view.ViewName == thisViewName);
            var otherViewIndex = views.FindIndex(view => view.ViewName == otherViewName);
            Assert.AreNotEqual(thisViewIndex, otherViewIndex);

            return otherViewIndex > thisViewIndex ? RelativePositionInArray.After : RelativePositionInArray.Before;
        }
    }
}