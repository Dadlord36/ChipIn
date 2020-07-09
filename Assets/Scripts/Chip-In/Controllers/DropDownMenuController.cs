using TMPro;
using UnityEngine;

namespace Controllers
{
    public class DropDownMenuController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;
        // public TMP_Dropdown.DropdownEvent selectedItemChanged;
        
        /*private void OnEnable()
        {
            dropdown.onValueChanged.AddListener(OnSelectedItemChanged);
        }

        private void OnDisable()
        {
            dropdown.onValueChanged.RemoveListener(OnSelectedItemChanged);
        }

        private void OnSelectedItemChanged(int selectedElementIndex)
        {
            selectedItemChanged.Invoke(selectedElementIndex);
        }*/


        public void Show()
        {
            dropdown.interactable = true;
            dropdown.Show();
            dropdown.interactable = false;
        }

        public void Hide()
        {
            dropdown.Hide();
        }
    }
}