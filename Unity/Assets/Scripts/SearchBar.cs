using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchBar : MonoBehaviour
{
    public GameObject bar;
    public bool buttonSearch = true;

    private void Start()
    {
        //buttonSearch = true;
        bar = GameObject.Find("InputField");
    }

    public void Button_Click()
    {
        //if (bar == null) Handheld.Vibrate();
        if (!buttonSearch) { // if user decides to search
            //Handheld.Vibrate();
            buttonSearch = true;
            bar.SetActive(true);
            
            //search for stuff
        }
        else if (buttonSearch) { //if the user searched
            buttonSearch = false;
            bar = GameObject.Find("InputField");
            bar.SetActive(false);

            //Handheld.Vibrate();
        }
    }

    public void Text_Changed(string newText)
    {
        doARsState.search = newText;
        doARsState.newSubmission = true;
    }
}
