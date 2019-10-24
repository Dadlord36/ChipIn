using System.Windows.Input;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Bindindgs
{
    [RequireComponent(typeof(Button))]
    public class ClickActionBinder : MonoBehaviour
    {
        private void Awake()
        {
            var button = GetComponent<Button>();
            Assert.IsNotNull(button);
            var command = GetComponent<ICommand>();
            Assert.IsNotNull(command);

            button.interactable = command.CanExecute(null);
            command.CanExecuteChanged+= delegate { button.interactable = command.CanExecute(null); };
            
            button.onClick.AddListener(delegate
            {
                command.Execute(null);
            });
        }
    }
}