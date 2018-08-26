using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class DjangoBridge : MonoBehaviour {

    private bool download = false;

    private void Start () {
        StartCoroutine(Download_World());
    }

    private void Update () {
        if (doARsState.current_state == doARs_state.downloading)
            download = true;
    }

    public IEnumerator Find_Worlds (string query) {
        
        if (doARsState.current_state == doARs_state.choose_world) {
            // Download the file from the URL. It will not be saved in the Cache
            string url = "http://www.arnocular.org/handle_doARs/show_worlds/";
            WWW www = new WWW(url);
            
            yield return www;
            if (www.error == null) {
                string list_data = www.text;
                Debug.Log(list_data);       
            } else {
                Debug.Log("ERROR: " + www.error);
            }
        }
    }

    public IEnumerator Download_World ()
    {
        yield return new WaitUntil(() => download);
        string world_name = doARsState.search;
        Debug.Log(world_name);
        // Download the file from the URL. It will not be saved in the Cache
        string BundleURL = "http://www.arnocular.org/handle_doARs/";
        // string BundleURL = "http://127.0.0.1:2567/handle_doARs/";
        UnityEngine.Networking.UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(BundleURL, 0);
        request.SetRequestHeader("world", world_name);
        yield return request.Send();
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        Debug.Log(bundle);
        if (world_name == "") {
            Instantiate(bundle.mainAsset);
        }
        else {
            Handheld.Vibrate();
            UnityEngine.Object[] list_of_worlds = bundle.LoadAllAssets();
            foreach (UnityEngine.Object world in list_of_worlds) {
                GameObject core = GameObject.Find("ModifiedARCore");
                Transform t = core.transform;
                t.position += new Vector3(0,0,5);
                UnityEngine.Object clone = Instantiate(world, t);
                GameObject f = GameObject.Find(clone.name);
                f.AddComponent<SelfDestruct>();
            }
            // Instantiate(bundle.LoadAsset(world_name));
        }
        // Unload the AssetBundles compressed contents to conserve memory
        Debug.Log("download asset bundle");
        bundle.Unload(false);
        doARsState.goToNextState();
        download = false;
    } // memory is freed from the web stream (www.Dispose() gets called implicitly)
        
    
}