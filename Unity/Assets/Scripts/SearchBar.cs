using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchBar : MonoBehaviour
{
    public GameObject bar;
    public InputField search;
    public bool buttonSearch = true;

    private void Start()
    {

    }

    public void Button_Click()
    {
        //if (bar == null) Handheld.Vibrate();
        if (!buttonSearch) { // if user decides to search
            //Handheld.Vibrate();
            buttonSearch = true;
            doARsState.goToState(doARs_state.choose_world);
            bar.SetActive(true);
            
            //search for stuff
        }
        else if (buttonSearch) { //if the user searched
            buttonSearch = false;
            bar = GameObject.Find("InputField");
            bar.SetActive(false);
            search = bar.GetComponent<InputField>();
            search.text = "";
            doARsState.newSubmission = true;
            doARsState.goToState(doARs_state.downloading);
            //Handheld.Vibrate();
        }
    }

    public void Text_Changed(string newText)
    {
        if (GameObject.Find("InputField") != null)
            doARsState.search = GameObject.Find("InputField").GetComponent<InputField>().text;
    }
}
