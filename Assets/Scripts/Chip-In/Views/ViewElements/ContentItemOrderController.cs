﻿using UnityEngine;
using UnityEngine.EventSystems;
using Views.ViewElements.Interfaces;

namespace Views.ViewElements
{
    public class ContentItemOrderController : UIBehaviour, IContentItemCanvasReceiver
    {
        [SerializeField] private float minOrder, maxOrder;

        public void UpdateContentItemCanvas(Transform contentItem, Canvas canvas, float pathPercentage)
        {
            canvas.sortingOrder = (int) Mathf.Lerp(minOrder, maxOrder, pathPercentage);
        }
    }
}