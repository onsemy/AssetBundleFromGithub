using System.Collections;
using System.Collections.Generic;
using JJFramework.Runtime;
using UnityEngine;

public class Test : MonoBehaviour
{
    private AssetBundleManager _assetBundleManager;

    private void Awake()
    {
        if (Caching.ClearCache())
        {
            Debug.Log("Cleaned!");
        }
        else
        {
            Debug.Log("No more clean");
        }
    }
    
    private IEnumerator OnClickStart()
    {
        _assetBundleManager = new AssetBundleManager();
        yield return _assetBundleManager.PrepareDownload("https://onsemy.github.io/AssetBundleFromGithubPage/Android/", "Android");
        Debug.Log($"Downloading AssetBundle Size: {_assetBundleManager.assetBundleTotalSize}");
        yield return _assetBundleManager.DownloadAllAssetBundle();
        yield return _assetBundleManager.PreloadAllAssetBundle();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Start"))
        {
            StartCoroutine(OnClickStart());
        }
        
        if (null != _assetBundleManager)
        {
            GUILayout.Label($"State: {_assetBundleManager.state}");
            if (_assetBundleManager.state == AssetBundleManager.STATE.DOWNLOADING)
            {
                GUILayout.Label($"Download Info: {_assetBundleManager.downloadedAssetBundleCount} / {_assetBundleManager.maximumAssetBundleCount}");
                GUILayout.Label($"Downloading: {_assetBundleManager.currentAssetBundleSize} ({_assetBundleManager.currentAssetBundleProgress * 100:0.0}%)");
            }
        }
        
    }
}
