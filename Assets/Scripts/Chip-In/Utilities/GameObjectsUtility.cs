using UnityEngine;
using UnityEngine.Assertions;

namespace Utilities
{
    public static class GameObjectsUtility
    {
        public static T FindOrAttach<T>(Transform rootTransform, in string objectName) where T : Component
        {
            var foundObject = rootTransform.Find(objectName);

            if (foundObject)
                return GetComponent<T>(foundObject);

            foundObject = new GameObject(objectName).transform;
            foundObject.SetParent(rootTransform);
            return GetComponent<T>(foundObject);
        }

        public static T GetComponent<T>(Transform owner) where T : Component
        {
            Assert.IsNotNull(owner);
            Assert.IsTrue(owner.TryGetComponent(out T rectTransform));
            return rectTransform;
        }
    }
}