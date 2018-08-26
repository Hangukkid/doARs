using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfDestruct : MonoBehaviour
{
    public string name;

    private void Start()
    {

    }

    private void Update () {
        if (doARsState.current_state == doARs_state.choose_world)
            Destroy(this.gameObject);
    }
}
