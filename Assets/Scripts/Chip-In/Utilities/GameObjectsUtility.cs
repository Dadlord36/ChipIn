using UnityEngine;

namespace Utilities
{
    public static class GameObjectsUtility
    {
        public static T FindOrAttach<T>(Transform rootTransform, in string objectName) where T : Component
        {
            T GetOrAttachComponent(GameObject gameObject)
            {
                return gameObject.TryGetComponent(out T component)
                    ? component
                    : gameObject.gameObject.AddComponent<T>();
            }

            var foundObject = rootTransform.Find(objectName);

            if (foundObject)
            {
                return GetOrAttachComponent(foundObject.gameObject);
            }

            foundObject = new GameObject(objectName).transform;
            foundObject.SetParent(rootTransform);

            return foundObject.gameObject.AddComponent<T>();
        }

        public static GameObject CreateAndAttachToParent(Transform parent, string objectName, Vector3 localPosition)
        {
            var newGameObject = new GameObject(objectName);
            newGameObject.transform.parent = parent;
            newGameObject.transform.localPosition = localPosition;
            return newGameObject;
        }

        public static void DestroyTransformAttachments(Transform slotSpinnerRootTransform, bool destroyImmediate = false)
        {
            var items = new GameObject[slotSpinnerRootTransform.childCount];

            for (int i = 0; i < slotSpinnerRootTransform.childCount; i++)
            {
                items[i] = slotSpinnerRootTransform.GetChild(i).gameObject;
            }

            slotSpinnerRootTransform.DetachChildren();

            if (destroyImmediate)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    Object.DestroyImmediate(items[i]);
                }
            }
            else
            {
                for (int i = 0; i < items.Length; i++)
                {
                    Object.Destroy(items[i]);
                }
            }
        }
    }
}