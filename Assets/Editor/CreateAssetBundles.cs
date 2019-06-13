using System.IO;
using UnityEngine;
using UnityEditor;

public class CreateAssetBundles
{

    [MenuItem("Worlds/AssetBundles/Build AssetBundles")]
    public static void BuildAssetBundles()
    {
        var thePath = Application.streamingAssetsPath;
        if (!Directory.Exists(thePath))
        {
            Directory.CreateDirectory(thePath);
        }
        BuildPipeline.BuildAssetBundles(thePath,
            BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows);
    }
    //[MenuItem("Worlds/AssetBundles/Build AssetBundles Mac")]
    //public static void BuildAssetBundlesMac()
    //{
    //    var thePath = Application.streamingAssetsPath +"/Mac"+ "/MonstersMod";
    //    if (!Directory.Exists(thePath))
    //    {
    //        Directory.CreateDirectory(thePath);
    //    }
    //    BuildPipeline.BuildAssetBundles(thePath,
    //        BuildAssetBundleOptions.None,
    //        BuildTarget.StandaloneOSXUniversal);
    //}
}
