
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;

namespace NBsn.NEditor 
{

public static class C_BsnAB
{
    [MenuItem("Bsn/Bsn/Set AB Name/Atlas")]
	private static void SetAtlasABName()
	{
		var listAtlasFileFullPaths = NBsn.NEditor.C_Path.GetAtlasFileFullPaths();
		List<string> listAssetsPaths = listAtlasFileFullPaths.FullPaths2AssetsPaths();
        foreach (var strAssetsPath in listAssetsPaths) {
            var importer = AssetImporter.GetAtPath(strAssetsPath);
            if (importer == null) {
                Debug.LogErrorFormat("basePath={0} importer == null", strAssetsPath);
                continue;
            }
            var strAssetsLatePath = strAssetsPath.PathAssets2AssetsLate().PathUpDir() + ".ab";
			// Debug.Log(strAssetsLatePath);
            importer.SetAssetBundleNameAndVariant(strAssetsLatePath, null);
        }
    }
    
	[MenuItem("Bsn/Bsn/Set AB Name/Prefab")]
	private static void SetPrefabABName()
	{
		var listPrefabFileFullPaths = NBsn.NEditor.C_Path.GetPrebabFileFullPaths();
		List<string> listAssetsPaths = listPrefabFileFullPaths.FullPaths2AssetsPaths();
        foreach (var strAssetsPath in listAssetsPaths) {
            var importer = AssetImporter.GetAtPath(strAssetsPath);
            if (importer == null) {
                Debug.LogErrorFormat("basePath={0} importer == null", strAssetsPath);
                continue;
            }
            var strAssetsLatePath = strAssetsPath.PathAssets2AssetsLate() + ".ab";
			// Debug.Log(strAssetsLatePath);
            importer.SetAssetBundleNameAndVariant(strAssetsLatePath, null);
        }
    }

	[MenuItem("Bsn/Bsn/Make AB/Win")]
	private static void MakeABWin() 
    {
		NBsn.CGlobal.EditorInit();
        string strFullPath = NBsn.PathConfig.ABLocalFullPath;
		NBsn.CGlobal.EditorUnInit();
        MakeAB(BuildTarget.StandaloneWindows64, strFullPath);
    }

	[MenuItem("Bsn/Bsn/Make AB/Android")]
	private static void MakeABAndroid() 
    {
		NBsn.CGlobal.EditorInit();
        string strFullPath = NBsn.PathConfig.ABLocalFullPath;
		NBsn.CGlobal.EditorUnInit();
        MakeAB(BuildTarget.Android, strFullPath);
    }

    public static void MakeAB(BuildTarget buildTarget, string strOutFullPath) {
        Debug.LogFormat("buildTarget={0} strOutFullPath={1}", buildTarget, strOutFullPath); 

        Directory.CreateDirectory(strOutFullPath);

        BuildPipeline.BuildAssetBundles(strOutFullPath, BuildAssetBundleOptions.UncompressedAssetBundle, buildTarget);
    } 
}

}
