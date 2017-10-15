
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

public static class C_Ver
{
    [MenuItem("Bsn/Bsn/3Ver/Lua", false, 1)]
	public static void LuaVer()
	{
        var luaFile2Base64Md5 = new Dictionary<string, string>();
        GetLuaFileInfo(ref luaFile2Base64Md5);
        LuaVer(ref luaFile2Base64Md5);
	}

    [MenuItem("Bsn/Bsn/3Ver/Win", false, 2)]
	public static void WinVer()
	{
        var utf8WithoutBom = new System.Text.UTF8Encoding(false);
        var strVer = File.ReadAllText(VerPath(), utf8WithoutBom);
        var strLuaVer = File.ReadAllText(LuaVerPath(), utf8WithoutBom);

        StringBuilder sb = new StringBuilder();
        sb.Append(strVer);
        sb.Append(strLuaVer);


        string strDirFullPath = Application.dataPath.PathCombine(
            NBsn.C_PathConfig.ServerResDirName
            , NBsn.C_Platform.GetName(0)
        );
        int nBaseLength = strDirFullPath.Length + 1;

        string strABFullPath = NBsn.C_PathConfig.AssetsDir.PathCombine(
			NBsn.C_PathConfig.ServerResDirName
			, NBsn.C_Platform.GetName(0)
			, NBsn.C_Platform.GetName(0)
		);

        var ab = AssetBundle.LoadFromFile(strABFullPath);
        var abManifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        var allAB = abManifest.GetAllAssetBundles();
            sb.AppendLine(allAB.Length.ToString());
        foreach (var item in allAB)
        {
            string strFilePath = strDirFullPath.PathCombine(item);
            var byData = File.ReadAllBytes(strFilePath);
            var byMd5 = NBsn.NCryptography.C_MD5.Compute(byData);
            var strMd5 = byMd5.Base64String();
            strFilePath = strFilePath.Substring(nBaseLength)
                .Replace(Path.DirectorySeparatorChar, '/')
                ;
            Debug.LogFormat("{0} {1}", strFilePath, strMd5);

            sb.Append(strFilePath);
            sb.Append(',');
            sb.AppendLine(strMd5);
        }
        ab.Unload(true);

        File.Delete(WinVerPath());
        File.WriteAllText(WinVerPath(), sb.ToString(), utf8WithoutBom);
	}

    [MenuItem("Bsn/Bsn/3Ver/Android", false, 3)]
	public static void AndroidVer()
	{
        var utf8WithoutBom = new System.Text.UTF8Encoding(false);
        var strVer = File.ReadAllText(VerPath(), utf8WithoutBom);
        var strLuaVer = File.ReadAllText(LuaVerPath(), utf8WithoutBom);

        StringBuilder sb = new StringBuilder();
        sb.Append(strVer);
        sb.Append(strLuaVer);

        File.Delete(AndriodVerPath());
        File.WriteAllText(AndriodVerPath(), sb.ToString(), utf8WithoutBom);
	}

    public static string VerPath()
	{
        return Application.dataPath.PathFormat()
            .PathCombine(C_PathConfig.ServerResDirName)
            .PathCombine(C_PathConfig.VerFileName)
            ;
	}

    public static string LuaVerPath()
	{
        return Application.dataPath.PathFormat()
            .PathCombine(C_PathConfig.AssetsLuaDirPath)
            .PathCombine(C_PathConfig.VerFileName)
            ;
	}

    public static string WinVerPath()
	{
        return Application.dataPath.PathFormat()
            .PathCombine(C_PathConfig.ServerResDirName)
            .PathCombine(C_Platform.GetName(0))
            .PathCombine(C_PathConfig.VerFileName)
            ;
	}

    public static string AndriodVerPath()
	{
        return Application.dataPath.PathFormat()
            .PathCombine(C_PathConfig.ServerResDirName)
            .PathCombine(C_Platform.GetName(1))
            .PathCombine(C_PathConfig.VerFileName)
            ;
	}

    public static void LuaVer(ref Dictionary<string, string> luaFile2Base64Md5)
	{
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(luaFile2Base64Md5.Count.ToString());
        foreach (var item in luaFile2Base64Md5) {
            sb.Append(item.Key);
            sb.Append(',');
            sb.AppendLine(item.Value);            
        }
        var utf8WithoutBom = new System.Text.UTF8Encoding(false);
        File.Delete(LuaVerPath());
        File.WriteAllText(LuaVerPath(), sb.ToString(), utf8WithoutBom);
	}

    public static void GetLuaFileInfo(ref Dictionary<string, string> file2Base64Md5)
    {
        file2Base64Md5.Clear();

        var strPath = Application.dataPath.PathFormat()
            .PathCombine(C_PathConfig.AssetsLuaDirPath)
            ;
        var nBaseLength = strPath.Length + 1;
        string[] arrFiles = Directory.GetFiles(strPath, "*.txt", SearchOption.AllDirectories);
        for (int i = 0; i < arrFiles.Length; ++i) {
            string strFilePath = arrFiles[i];
            var byData = File.ReadAllBytes(strFilePath);
            var byMd5 = NBsn.NCryptography.C_MD5.Compute(byData);
            var strMd5 = byMd5.Base64String();
            strFilePath = strFilePath.Substring(nBaseLength)
                .Replace(Path.DirectorySeparatorChar, '/')
                ;
            Debug.LogFormat("{0} {1}", strFilePath, strMd5);
            file2Base64Md5.Add(strFilePath, strMd5);
        }
    }
}

}
