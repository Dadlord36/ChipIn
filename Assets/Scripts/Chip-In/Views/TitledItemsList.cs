using System;
using UnityEngine;
using Utilities;
using Views.Bars.BarItems;
using Views.ViewElements.Lists;

namespace Views
{
    public class TitledItemsList : MonoBehaviour
    {
        private const string Tag = nameof(TitledItemsList);
        
        [SerializeField] private Transform container;
        [SerializeField] private GameObject prefab;

        public void Fill(ITitled[] titledItemsData, Action<string> onItemSelected)
        {
            if (titledItemsData == null)
            {
                LogUtility.PrintLog(Tag,"There is no items to show");
                return;
            }
            
            for (int i = 0; i < titledItemsData.Length; i++)
            {
                var instance = Instantiate(prefab, container);
                var element = instance.GetComponent<TitledElement>();
                element.Title = titledItemsData[i].Title;
                element.WasSelected += onItemSelected;
            }
        }

        public void SubscribeOnElementsSelection(Action<string> onItemSelected)
        {
            foreach (Transform child in container.transform)
            {
                child.GetComponent<TitledElement>().WasSelected += onItemSelected;
            }
        }
    }
}