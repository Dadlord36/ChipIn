using UnityEngine;
using UnityEngine.Assertions;

namespace Views
{
    public abstract class ContainerView<T> : BaseView where T:Object 
    {
        [SerializeField] private Transform itemsContainer;
        [SerializeField] private T itemPrefab;
        
        public ContainerView(string tag) : base(tag)
        {
        }
        
        public T AddItem()
        {
            Assert.IsNotNull(itemsContainer);
            return Instantiate(itemPrefab, itemsContainer);
        }

        public void RemoveAllItems()
        {
            foreach (Transform child in itemsContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}