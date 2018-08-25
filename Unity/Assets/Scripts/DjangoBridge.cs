using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DjangoBridge : MonoBehaviour {

    private void Start () {

    }

    private void Update () {

    }

    public IEnumerator Find_Worlds (string query) {
        if (doARsState.current_state == doARs_state.choose_world) {
            // Download the file from the URL. It will not be saved in the Cache
            string url = "http://127.0.0.1:2567/handle_doARs/show_worlds/";
            WWW www = new WWW(url);
            
            yield return www;
            if (www.error == null) {
                if (doARsState.current_state == doARs_state.downloading) {
                    string list_data = www.text;
                    Debug.Log(list_data);
                    doARsState.goToNextState();
                }
            } else {
                Debug.Log("ERROR: " + www.error);
            }
        }
    }

    public IEnumerator Download_World (string world_name)
    {
        if (doARsState.current_state == doARs_state.downloading) {
            // Download the file from the URL. It will not be saved in the Cache
            string BundleURL = "http://127.0.0.1:2567/handle_doARs/";
            var headers = new Dictionary<string, string>();
            headers.Add("world", world_name);
            using (WWW www = new WWW(BundleURL, null, headers))
            {
                yield return www;
                if (www.error != null)
                    Debug.Log("WWW download had an error:" + www.error);
                AssetBundle bundle = www.assetBundle;
                if (world_name == "")
                    Instantiate(bundle.mainAsset);
                else
                    Instantiate(bundle.LoadAsset(world_name));
                // Unload the AssetBundles compressed contents to conserve memory
                Debug.Log("download asset bundle");
                bundle.Unload(false);
            } // memory is freed from the web stream (www.Dispose() gets called implicitly)
        }
    }
}