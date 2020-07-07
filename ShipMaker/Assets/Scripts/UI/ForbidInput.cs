using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;


public class ForbidInput : MonoBehaviour
{
    public InputField inputName;


    void OnGUI()
    {
        inputName.text = Regex.Replace(inputName.text, @"[^a-zA-Z0-9°()]", "");
    }
}