
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
    [MenuItem("Bsn/Ver/Lua", false, 1)]
	public static void LuaVer()
	{
        var luaFile2Base64Md5 = new Dictionary<string, string>();
        GetLuaFileInfo(ref luaFile2Base64Md5);
        LuaVer(ref luaFile2Base64Md5);
	}

    [MenuItem("Bsn/Ver/Win", false, 2)]
	public static void WinVer()
	{
        var strVer = File.ReadAllText(VerPath());
        var strLuaVer = File.ReadAllText(LuaVerPath());

        StringBuilder sb = new StringBuilder();
        sb.AppendLine();
        sb.Append(strVer);
        sb.Append(strLuaVer);

        File.Delete(WinVerPath());
        File.WriteAllText(WinVerPath(), sb.ToString(), System.Text.Encoding.UTF8);
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

    public static void LuaVer(ref Dictionary<string, string> luaFile2Base64Md5)
	{
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(C_PathConfig.LuaDirName);
        sb.AppendLine(luaFile2Base64Md5.Count.ToString());
        foreach (var item in luaFile2Base64Md5) {
            sb.Append(item.Key);
            sb.Append(',');
            sb.AppendLine(item.Value);            
        }

        File.Delete(LuaVerPath());
        File.WriteAllText(LuaVerPath(), sb.ToString(), System.Text.Encoding.UTF8);
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
