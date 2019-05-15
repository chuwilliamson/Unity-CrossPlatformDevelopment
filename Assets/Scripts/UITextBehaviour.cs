using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UITextBehaviour : MonoBehaviour
{
    public Variables.Variable variableRef;
    private Text _textComponent;

    private void Awake()
    {
        _textComponent = GetComponent<Text>();
    }

    private void Update()
    {
        _textComponent.text = variableRef.ToString();
    }
}