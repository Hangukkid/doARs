using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchBar : MonoBehaviour
{
    public GameObject bar;
    public bool buttonSearch = false;
    
    public void Button_Click()
    {
        if (!buttonSearch) { // if user decides to search
            buttonSearch = true;
            bar.SetActive(true);
            //search for stuff
        } else if (buttonSearch) { //if the user searched
            bar.SetActive(false);
            buttonSearch = false;
        }
    }

    public void Text_Changed(string newText)
    {
        
    }
}
