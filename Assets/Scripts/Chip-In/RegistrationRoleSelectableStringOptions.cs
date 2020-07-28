using GlobalVariables;

public sealed class RegistrationRoleSelectableStringOptions : SelectableStringOptions
{
    public RegistrationRoleSelectableStringOptions()
    {
        options = new[] {MainNames.UserRoles.Client, MainNames.UserRoles.BusinessOwner};
    }
}