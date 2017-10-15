
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
    [MenuItem("Bsn/Bsn/1Set AB Name/All", false, 1)]
	private static void SetAllABName()
	{
		SetAtlasABName();
		SetPrefabABName();
	}

    [MenuItem("Bsn/Bsn/1Set AB Name/Atlas", false, 2)]
	private static void SetAtlasABName()
	{
		var nPrefixLength = NBsn.C_PathConfig.ServerResABResHttpDirName.Length + 1;
		var listAtlasFileFullPaths = NBsn.NEditor.C_Path.GetABResAtlasFileFullPaths();
		List<string> listAssetsPaths = listAtlasFileFullPaths.FullPaths2AssetsPaths();
        foreach (var strAssetsPath in listAssetsPaths)
        {
            if (strAssetsPath.PathExtension() == ".tpsheet")
            {
                continue;
            }

            var importer = AssetImporter.GetAtPath(strAssetsPath);
            if (importer == null) {
                Debug.LogErrorFormat("basePath={0} importer == null", strAssetsPath);
                continue;
            }

			var strABPath = strAssetsPath.Substring(nPrefixLength) + NBsn.C_Config.ABSuffix;
            importer.SetAssetBundleNameAndVariant(strABPath, null);
        }
    }
    
	[MenuItem("Bsn/Bsn/1Set AB Name/Prefab", false, 3)]
	private static void SetPrefabABName()
	{
		var nPrefixLength = NBsn.C_PathConfig.ServerResABResHttpDirName.Length + 1;
		var listPrefabFileFullPaths = NBsn.NEditor.C_Path.GetABResPrebabFileFullPaths();
		List<string> listAssetsPaths = listPrefabFileFullPaths.FullPaths2AssetsPaths();
        foreach (var strAssetsPath in listAssetsPaths) {
            var importer = AssetImporter.GetAtPath(strAssetsPath);
            if (importer == null) {
                Debug.LogErrorFormat("basePath={0} importer == null", strAssetsPath);
                continue;
            }

			var strABPath = strAssetsPath.Substring(nPrefixLength) + NBsn.C_Config.ABSuffix;
            importer.SetAssetBundleNameAndVariant(strABPath, null);
        }
    }


	[MenuItem("Bsn/Bsn/2Make AB/Win64", false, 1)]
	private static void MakeABWin64() 
    {
        MakeAB(BuildTarget.StandaloneWindows64);
    }

	[MenuItem("Bsn/Bsn/2Make AB/Android", false, 2)]
	private static void MakeABAndroid() 
    {
        MakeAB(BuildTarget.Android);
    }

    public static void MakeAB(BuildTarget buildTarget) {
        Debug.LogFormat("buildTarget={0}", buildTarget); 

		var strOutFullPath = GetABFullPath(buildTarget);
		MakeAB(buildTarget, strOutFullPath);
    } 

    public static void MakeAB(BuildTarget buildTarget, string strOutFullPath) {
        Debug.LogFormat("buildTarget={0} strOutFullPath={1}", buildTarget, strOutFullPath); 

        Directory.CreateDirectory(strOutFullPath);
        BuildPipeline.BuildAssetBundles(strOutFullPath, BuildAssetBundleOptions.UncompressedAssetBundle, buildTarget);
    } 

	public static string GetABFullPath(BuildTarget buildTarget) {
        Debug.LogFormat("buildTarget={0}", buildTarget); 

		var strPlatform = NBsn.NEditor.CPlatform.Name(buildTarget);
		var strPlatformABOutAssetsPath = NBsn.C_PathConfig.AssetsLatePlatformABOutPath(strPlatform);
		var strOutFullPath = Application.dataPath.PathCombine(strPlatformABOutAssetsPath).PathFormat();
		return strOutFullPath;
    } 
}

}
