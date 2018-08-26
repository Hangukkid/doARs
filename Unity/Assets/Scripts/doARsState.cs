using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum doARs_state { choose_world, downloading, rest };

/*
A class dedicated to ordering the operations done by the app.
First user chooses a city
Downloading happens
The prefabs are loaded (is paused using a queue that doesn't run until it's in the setup state)
rest -> rest of the app
     */

public static class doARsState
{
    public static doARs_state current_state = doARs_state.choose_world;
    public static List<string> worlds;

    public static List<string> found_worlds;

    public static string search = null;
    public static bool newSubmission = false;

    public static doARs_state getNextState()
    {
        switch (current_state)
        {
            case doARs_state.choose_world:
                return doARs_state.downloading;
            case doARs_state.downloading:
                return doARs_state.rest;
            default:
                Debug.Log("error no next state");
                return current_state;
        }
    }

    public static void goToNextState()
    {
        current_state = getNextState();
        Debug.Log("changed state to " + current_state);
    }

    
    public static void goToState(doARs_state state)
    {
        current_state = state;
        Debug.Log("changed state to " + current_state);
    }
}
