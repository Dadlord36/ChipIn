using System;
using System.Collections.Generic;
using UnityEngine;

namespace Repositories.Local
{
    [Serializable]
    public struct IconEllipseData
    {
        public string name;
        public Sprite sprite;
        public float scale;
    }


    [CreateAssetMenu(fileName = nameof(IconEllipsesRepository),
        menuName = nameof(Repositories) + "/" + nameof(Local) + "/" + nameof(IconEllipsesRepository), order = 0)]
    public class IconEllipsesRepository : ScriptableObject
    {
        [SerializeField] private List<IconEllipseData> ellipsesData;

        public string[] ElementsNames { get; private set; }

        private void OnValidate()
        {
            var count = ellipsesData.Count;
            ElementsNames = new string[count];

            for (int i = 0; i < count; i++)
            {
                ElementsNames[i] = ellipsesData[i].name;
            }
        }

        public IconEllipseData GetEllipse(string ellipseName)
        {
            return ellipsesData.Find(data => data.name == ellipseName);
        }

        public IconEllipseData this[int index] =>  ellipsesData[index];
    }
}