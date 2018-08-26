using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ViewModel : MonoBehaviour 
{
    public Text buttonText;
    public GameObject cube;
    public Slider slider;

    public void Button_Click()
    {
        Debug.Log("Hello, World!!!!!!!");
    }

    public void Text_Changed(string newText)
    {
        Debug.Log(newText);
    }

}
