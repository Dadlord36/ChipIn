using UnityEngine;

namespace Utilities
{
    public static class GameObjectsUtility
    {
        public static T FindOrAttach<T>(Transform rootTransform, in string objectName) where T : Component
        {
            T GetOrAttachComponent(GameObject gameObject)
            {
                if (gameObject.TryGetComponent(out T component))
                    return component;
                return gameObject.gameObject.AddComponent<T>();
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
    }
}