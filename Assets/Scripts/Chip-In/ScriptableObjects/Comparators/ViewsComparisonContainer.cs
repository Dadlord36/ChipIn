using System;
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

        int GetViewIndex(string viewName) => views.FindIndex(view => view.ViewName == viewName);


        public RelativePositionInArray GetRelativePositionInContainer(string thisViewName, string otherViewName)
        {
            var thisViewIndex = GetViewIndex(thisViewName);
            var otherViewIndex = GetViewIndex(otherViewName);

            Assert.AreNotEqual(thisViewIndex, otherViewIndex);

            return otherViewIndex > thisViewIndex ? RelativePositionInArray.After : RelativePositionInArray.Before;
        }

        public bool GetRelativeViewName(string relativeViewName, RelativePositionInArray relativePositionInArray, out string correspondingView)
        {
            correspondingView = null;
            var viewIndex = GetViewIndex(relativeViewName);
            int correspondingIndex;

            switch (relativePositionInArray)
            {
                case RelativePositionInArray.Before:
                    correspondingIndex = viewIndex - 1;
                    break;
                case RelativePositionInArray.After:
                    correspondingIndex = viewIndex + 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(relativePositionInArray), relativePositionInArray, null);
            }

            if (correspondingIndex >= 0 && correspondingIndex < views.Count)
            {
                correspondingView = views[correspondingIndex].ViewName;
                return true;
            }

            return false;
        }
    }
}