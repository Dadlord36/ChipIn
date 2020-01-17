using System.Collections.Generic;
using UnityEngine;
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
            return views.Exists(baseView => baseView.GetViewName == view.GetViewName);
        }
    }
}