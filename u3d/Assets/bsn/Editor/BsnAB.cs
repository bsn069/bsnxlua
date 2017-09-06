
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
    [MenuItem("Bsn/Bsn/Set AB Name/All")]
	private static void SetAllABName()
	{
		SetAtlasABName();
		SetPrefabABName();
		SetLuaABName();
	}

    [MenuItem("Bsn/Bsn/Set AB Name/Atlas")]
	private static void SetAtlasABName()
	{
        //var strABResPath = NBsn.C_PathConfig.AssetsABResPath + Path.DirectorySeparatorChar;

		var listAtlasFileFullPaths = NBsn.NEditor.C_Path.GetABResAtlasFileFullPaths();
		List<string> listAssetsPaths = listAtlasFileFullPaths.FullPaths2AssetsPaths();
        foreach (var strAssetsPath in listAssetsPaths) {
            var importer = AssetImporter.GetAtPath(strAssetsPath);
            if (importer == null) {
                Debug.LogErrorFormat("basePath={0} importer == null", strAssetsPath);
                continue;
            }

			var strABPath = strAssetsPath + NBsn.C_Config.ABSuffix;
            importer.SetAssetBundleNameAndVariant(strABPath, null);
        }
    }
    
	[MenuItem("Bsn/Bsn/Set AB Name/Prefab")]
	private static void SetPrefabABName()
	{
        //var strABResPath = NBsn.C_PathConfig.AssetsABResPath + Path.DirectorySeparatorChar;

		var listPrefabFileFullPaths = NBsn.NEditor.C_Path.GetABResPrebabFileFullPaths();
		List<string> listAssetsPaths = listPrefabFileFullPaths.FullPaths2AssetsPaths();
        foreach (var strAssetsPath in listAssetsPaths) {
            var importer = AssetImporter.GetAtPath(strAssetsPath);
            if (importer == null) {
                Debug.LogErrorFormat("basePath={0} importer == null", strAssetsPath);
                continue;
            }

			var strABPath = strAssetsPath + NBsn.C_Config.ABSuffix;
            importer.SetAssetBundleNameAndVariant(strABPath, null);
        }
    }

	[MenuItem("Bsn/Bsn/Set AB Name/Lua")]
	private static void SetLuaABName()
	{
        //var strABResPath = NBsn.C_PathConfig.AssetsABResPath + Path.DirectorySeparatorChar;

		string strABResLuaFullPath = Application.dataPath.PathFormat().PathCombine(NBsn.C_PathConfig.ABResDir).PathCombine(NBsn.C_PathConfig.ABResLuaDir);

		string[] strFileFullPaths = Directory.GetFiles(strABResLuaFullPath, "*.txt", SearchOption.AllDirectories);
        if (strFileFullPaths != null) 
		{
            for (int i = 0; i < strFileFullPaths.Length; ++i)
			{
                string strAssetsPath = strFileFullPaths[i].FullPathToAssetsPath();

				var importer = AssetImporter.GetAtPath(strAssetsPath);
				if (importer == null) {
					Debug.LogErrorFormat("basePath={0} importer == null", strAssetsPath);
					continue;
				}

				var strABPath = strAssetsPath + NBsn.C_Config.ABSuffix;
				importer.SetAssetBundleNameAndVariant(strABPath, null);
            }
        }
    }

	[MenuItem("Bsn/Bsn/Make AB/Win64")]
	private static void MakeABWin64() 
    {
        MakeAB(BuildTarget.StandaloneWindows64);
    }

	[MenuItem("Bsn/Bsn/Make AB/Android")]
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
