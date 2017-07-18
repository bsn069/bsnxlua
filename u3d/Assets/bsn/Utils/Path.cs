using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn {

public static class Utils  
{
	public static string GetRelativeAssetsPath(string path) 
	{
		return "Assets" + Path.GetFullPath(path).Replace(Path.GetFullPath(Application.dataPath), "").Replace('\\', '/');
	}

	// Assets\ABRes\Package\Prefab\UI\UITest.prefab -> Assets/ABRes/Package/Prefab/UI/UITest.prefab 
	public static string FormatPathSplitChar(string path) 
	{
		return path.Replace('\\', '/');
	}

	// 返回全路径的Assets路径
    // path ["H:/dev/swordm3d-code/trunk/client/swordm3d/Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab", ...]
    // ret ["Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab", ...]
    public static List<string> FullPath2AssetsPath(List<string> path) 
    {
        var subStart = Application.dataPath.Length - "Assets".Length;
        List<string> ret = new List<string>();
        foreach (var item in path) {
            ret.Add(item.Substring(subStart));
        }
        return ret;
    }

    // 返回全路径的Assets后的路径
    // path "H:/dev/swordm3d-code/trunk/client/swordm3d/Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab"
    // ret "_Game/Resources/Packages/UI/EquipAvartarTip.prefab"
    public static string FullPath2AssetsLatePath(string path) 
    {
        var subStart = Application.dataPath.Length;
        return path.Substring(subStart);
    }

    // 返回Assets路径的Assets后的路径 (Assets/* => *)
    // path "Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab"
    // ret "_Game/Resources/Packages/UI/EquipAvartarTip.prefab"
    public static string AssetsPath2AssetsLatePath(string path) 
    {
        return path.Substring("Assets".Length + 1);
    }

    // 返回Assets路径的Assets后去除文件的路径 (Assets/*/? => *)
    // path "Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab"
    // ret "_Game/Resources/Packages/UI"
    public static string AssetsPath2AssetsLatePathNoFile(string strPath) 
    {
        var nLastIndex = strPath.LastIndexOf('/');
        var strBefore = strPath.Substring(0, nLastIndex);
        var strAfter = strBefore.Substring("Assets".Length + 1);
        return strAfter;
    }
}  

}