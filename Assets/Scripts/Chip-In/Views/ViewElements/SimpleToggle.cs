using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.ViewElements
{
    public class SimpleToggle : UIBehaviour, IPointerClickHandler
    {
#if UNITY_EDITOR
        public bool UiElementsReferencesAreValid => labelTextField != null;
#endif

        [SerializeField] private TMP_Text labelTextField;

        public string LabelText
        {
            get => labelTextField.text;
            set => labelTextField.text = value;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Was CLICKED");
        }
    }
}