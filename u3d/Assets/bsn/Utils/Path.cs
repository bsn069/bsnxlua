using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;

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
}  

}