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

        private void OnEnable()
        {
            _textComponent = GetComponent<Text>();
            var prefix = _textComponent.text;
            variableRef.OnValueChange.AddListener((obj) => { _textComponent.text = prefix + obj.ToString();});
        }

    }
}