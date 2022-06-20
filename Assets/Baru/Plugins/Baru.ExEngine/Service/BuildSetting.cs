using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BuildSetting : MonoBehaviour
{
    public enum PublisherType : byte 
    {
        Self,
        Max
    }

    public enum BuildServerType : byte 
    {
        Trunk,
        Release,
        Live,
        Max
    }

    [System.Serializable]
    public class AssetBundlePathInfo
    {
        public RuntimePlatform mRunTimePlatform;
        public string mPath;
    }

    public PublisherType mPublicsherType = PublisherType.Self;
    public BuildServerType mBildServerType = BuildServerType.Trunk;
    public bool mStreamingAssets = false;
    public bool mStreamingAssetsTables = false;
    public List<AssetBundlePathInfo> mAssetBundlePathInfos;
    public bool mAppSplit = false;
    public string mBuildTime;

    public string mVersionName = "0.0.0"; // Season.Major.Minor
    public string mVersionCode = "0";
    public string mTestVersionIndex = "0";

    public string BundleID_Android;
    public string BundleID_iOS;
}
