using ScriptableObjects.ActionsConnectors;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace UI
{
    public class ConnectedTextChanger : UIBehaviour
    {
        [SerializeField] private ValueConnector valueConnector;
        [SerializeField] private TextMeshProUGUI text;

        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(valueConnector);
            Assert.IsNotNull(text);
            valueConnector.ValueChanged += delegate(int value) { text.text = value.ToString(); };
        }
    }
}