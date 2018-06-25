using System;
using UnityEngine;
using UnityEngine.UI;

public class RunChoiceBehaviour : MonoBehaviour
{
    public Text InformationText;

    public void Run()
    {
        const string info = "Run Choice Selected I am running....";
        InformationText.text += info + Environment.NewLine;
    }
}