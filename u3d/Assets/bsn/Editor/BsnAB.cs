
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;
using XLua;

namespace NBsn.NEditor 
{

public static class C_BsnAB
{
    [MenuItem("BsnBuild/1Set AB Name", false, 101)]
	private static void SetABName()
	{
		NBsn.C_Global.EditorBegin();
		LuaTable table = NBsn.C_Global.Instance.Lua.NewTable();
		NBsn.C_Global.Instance.Lua.DoString("require 'editor.ab_config'");
		NBsn.C_Global.Instance.Lua.Get("g_tTemp", out table);
		table.ForEach<string, LuaTable>((strPath, tb)=>{
			string strAB;
			tb.Get("strAssetBundleOutPath", out strAB);
			NBsn.C_Global.Instance.Log.InfoFormat("{0} -> {1}", strPath, strAB);

			var strAssetsPath = strPath.PathFormat();
            var importer = AssetImporter.GetAtPath(strAssetsPath);
            if (importer == null) {
                Debug.LogErrorFormat("basePath={0} importer == null", strAssetsPath);
                return;
            }

			var strABPath = strAB;
            importer.SetAssetBundleNameAndVariant(strABPath, null);
		});

		NBsn.C_Global.EditorEnd();
    }

	[MenuItem("BsnBuild/2Make AB/Win64", false, 201)]
	private static void MakeABWin64() 
    {
        MakeAB(BuildTarget.StandaloneWindows64);
    }

	[MenuItem("BsnBuild/2Make AB/Android", false, 202)]
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
		var strOutFullPath = NBsn.C_PathConfig.EditorAssetsFullPath.PathCombine(
			NBsn.C_PathConfig.ServerResDirName
			, strPlatform
			, NBsn.C_PathConfig.ABResDirName
			).PathFormat();
		return strOutFullPath;
    } 
}

}
