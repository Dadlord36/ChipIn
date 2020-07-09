using UnityEngine;

namespace ScriptableObjects.Validations
{
    [CreateAssetMenu(fileName = nameof(PasswordValidation),
        menuName = "Validations/" + nameof(PasswordValidation), order = 0)]
    public class PasswordValidation : DataModelValidation<string>
    {
        [SerializeField] private byte minNumberOfPasswordCharacters;
        public override bool CheckIsValid(string password)
        {
            return CheckPasswordIsValid(password);
        }
        
        private  bool CheckPasswordIsValid(string password)
        {
            return !string.IsNullOrEmpty(password)  && password.Length >= minNumberOfPasswordCharacters;
        }
    }
}