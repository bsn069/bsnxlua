
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

public static class C_Path
{
	/* 获取ABRes\Atlas下的图集文件全路径
    [
		"F:\github\bsnxlua\u3d\Assets\ABRes\Atlas\gameui1\duiwurenshu.png", 
		"F:\github\bsnxlua\u3d\Assets\ABRes\Atlas\gameui1\duiwu_1_1.png",
	...]
	*/
	public static List<string> GetABResAtlasFileFullPaths()
	{
		string strABResAtlasFullPath = Application.dataPath.PathFormat().PathCombine(NBsn.C_PathConfig.ABResDir).PathCombine(NBsn.C_PathConfig.ABResAtlasDir);
		return GetNoMetaFileFullPaths(strABResAtlasFullPath);
    }

	/* 获取ABRes\Prefab下的prefab文件全路径
    [
		"F:\github\bsnxlua\u3d\Assets\ABRes\Prefab\UI\a.prefab", 
		"F:\github\bsnxlua\u3d\Assets\ABRes\Prefab\UI\b.prefab",
	...]
	*/
	public static List<string> GetABResPrebabFileFullPaths()
	{
		string strPrefabFullPath = Application.dataPath.PathFormat().PathCombine(NBsn.C_PathConfig.ABResDir).PathCombine(NBsn.C_PathConfig.ABResPrefabDir);
		return GetNoMetaFileFullPaths(strPrefabFullPath);
    }



	public static List<string> GetNoMetaFileFullPaths(string strFullPath)
	{
		// Debug.Log(strFullPath);		
		List<string> fileFullPaths  = new List<string>();

        string[] strFileFullPaths = Directory.GetFiles(strFullPath, "*", SearchOption.AllDirectories);
        if (strFileFullPaths != null) 
		{
            for (int i = 0; i < strFileFullPaths.Length; ++i)
			{
                string strFileFullPath = strFileFullPaths[i];
				if (strFileFullPath.EndsWith(".meta"))
				{
					continue;
                }
				fileFullPaths.Add(strFileFullPath);
				Debug.Log(strFileFullPath);
            }
        }

		return fileFullPaths;
    }


    // 获取path里会依赖其它资源的资源
    // ["H:/dev/swordm3d-code/trunk/client/swordm3d/Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab", ...]
    public static List<string> GetDependenciesRes(string path)
	{
        //Debug.LogFormat("GetDependenciesRes({0})", path);
        List<string> allFiles = new List<string>();
        string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
        if (files != null) {
            for (int i = 0; i < files.Length; ++i) {
                string file = files[i];
                if (file.EndsWith(".cs")
                    || file.EndsWith(".meta")
                    || file.EndsWith(".png")
                ) {
                    continue;
                }
                file = file.Replace('\\', '/');
                allFiles.Add(file);
            }
        }
        return allFiles;
    }


    // 获取path里可以设置ab名称的资源
    // ["H:/dev/swordm3d-code/trunk/client/swordm3d/Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab", ...]
    public static List<string> GetCanSetABNameRes(string path)
	{
        //Debug.LogFormat("GetDependenciesRes({0})", path);
        List<string> allFiles = new List<string>();
        string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
        if (files != null) {
            for (int i = 0; i < files.Length; ++i) {
                string file = files[i];
                if (file.EndsWith(".cs")
                    || file.EndsWith(".meta")
                    || file.EndsWith(NBsn.C_Config.ABSuffix)
                ) {
                    continue;
                }
                file = file.Replace('\\', '/');
                allFiles.Add(file);
            }
        }
        return allFiles;
    }
}

}
