using System;
using UnityEngine;

namespace ActionsTranslators
{
    public interface IUpdatable
    {
        void Update();
    }
    
    [CreateAssetMenu(fileName = nameof(MainInputActionsTranslator), menuName = nameof(ActionsTranslators) + "/" + nameof(MainInputActionsTranslator),
        order = 0)]
    public sealed class MainInputActionsTranslator : ScriptableObject, IUpdatable
    {
        public event Action EscapeButtonPressed;
        void IUpdatable.Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnReturnButtonPressed();
            }
        }

        private void OnReturnButtonPressed()
        {
            EscapeButtonPressed?.Invoke();
        }
    }
}