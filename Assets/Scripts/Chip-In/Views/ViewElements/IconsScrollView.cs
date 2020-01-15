using UnityEngine;
using UnityEngine.EventSystems;
using WebOperationUtilities;

namespace Views.ViewElements
{
    public class IconsScrollView : UIBehaviour
    {
        [SerializeField] private IconElementView itemPrefab;
        [SerializeField] private Transform container;

        public void AddElement(Sprite sprite)
        {
            CreateIconElementInstance().Icon = sprite;
        }

        public void AddElement(byte[] iconTextureData)
        {
            CreateIconElementInstance().Icon = SpritesUtility.CreateSpriteWithDefaultParameters(iconTextureData);
        }

        private IconElementView CreateIconElementInstance()
        {
            return Instantiate(itemPrefab, container, true);
        }

        public void RemoveAllItems()
        {
            foreach (Transform item in container)
            {
                Destroy(item.gameObject);
            }
        }
    }
}