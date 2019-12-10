using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Behaviours.Games
{
    public sealed class Coin : MonoBehaviour, IInteractiveValue, IPointerClickHandler
    {
        public event Action<int> Collected;

        [SerializeField] private TextMeshProUGUI valueMultiplierTextField;
        [SerializeField] private int value;

#if UNITY_EDITOR
        
        private void OnValidate()
        {
            if (Application.isPlaying) return;
            valueMultiplierTextField.text = $"x{value.ToString()}";
        }
#endif

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnCollected(value);
            MakeFinishingAction();
        }

        private void MakeFinishingAction()
        {
            gameObject.SetActive(false);
        }

        private void OnCollected(int obj)
        {
            Collected?.Invoke(obj);
        }
    }
}