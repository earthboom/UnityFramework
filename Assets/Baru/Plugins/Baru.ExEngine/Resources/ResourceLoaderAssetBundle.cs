using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoaderAssetBundle : MonoBehaviour
{
    Dictionary<string, AssetBundle> mAssetBundles = new Dictionary<string, AssetBundle>();

    Dictionary<string, Object> mLoadedAssets = new Dictionary<string, Object>();

    readonly string[] gKeySeparator = new string[] { "-name:" };
}
