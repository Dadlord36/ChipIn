using System;
using GlobalVariables;
using UnityEngine;
using UnityEngine.Events;
using UnityWeld.Binding;
using ViewModels.UI.Elements.Buttons;

[Binding]
public sealed class RegistrationRoleSelector : MonoBehaviour
{
    [Serializable]
    public class StringUnityEvent : UnityEvent<string>{} 
    
    [SerializeField] private StringUnityEvent roleChanged;
    
    [SerializeField] private GroupedHighlightedButton selectedByDefaultButton;
    private string _registrationRole;

    [Binding]
    public string RegistrationRole
    {
        get => _registrationRole;
        set
        {
            if (value == _registrationRole) return;
            _registrationRole = value;
            OnRoleChanged(value);
        }
    }

    [Binding]
    public void SelectUserRole()
    {
        RegistrationRole = MainNames.UserRoles.Client;
    }

    [Binding]
    public void SelectBusinessOwnerRole()
    {
        RegistrationRole = MainNames.UserRoles.BusinessOwner;
    }
    
    private void OnEnable()
    {
        selectedByDefaultButton.PerformGroupActionWithoutNotification();
    }

    private void OnRoleChanged(string obj)
    {
        roleChanged?.Invoke(obj);
    }
}