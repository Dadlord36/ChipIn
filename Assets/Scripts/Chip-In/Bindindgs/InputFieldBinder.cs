using System.Windows.Input;
using TMPro;
using UnityEngine;

namespace Bindindgs
{
    [RequireComponent(typeof(TMP_InputField))]
    public class InputFieldBinder : MonoBehaviour
    {
        private void Awake()
        {
            var command = GetComponent<ICommand>();
            var inputField = GetComponent<TMP_InputField>();
            
            inputField.interactable = command.CanExecute(inputField.text);
            
            command.CanExecuteChanged += delegate
            {
                inputField.interactable = !inputField.interactable;
            };
            inputField.onSubmit.AddListener(delegate(string text)
            {
                if (command.CanExecute(null))
                {
                    command.Execute(text);
                }
            });
        }
    }
}