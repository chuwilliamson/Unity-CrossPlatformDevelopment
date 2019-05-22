using Chuwilliamson.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Chuwilliamson.CrossPlatformDevelopment
{
    [RequireComponent(typeof(Text))]
    public class UITextBehaviour : MonoBehaviour
    {
        private Text _textComponent;
        public Variable variableRef;

        private void Awake()
        {
            _textComponent = GetComponent<Text>();
        }

        private void Update()
        {
            _textComponent.text = variableRef.ToString();
        }
    }
}