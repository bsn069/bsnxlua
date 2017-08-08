using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn {

public static class StringEx  
{
	public static byte[] UTF8Bytes(this string strData)
	{
		byte[] bytes = System.Text.Encoding.UTF8.GetBytes(strData);
        return bytes;
	}

	// 返回全路径的Assets后的路径
	// strFullPath = F:\github\bsnxlua\u3d\Assets\bsn\Utils\CommonEx.cs
	// ret bsn\Utils\CommonEx.cs
	public static string FullPath2AssetsLatePath(this string strFullPath) 
	{
		return strFullPath.Substring(ms_dataPathLength);
	}

	// 返回Assets路径后的路径
	// strAssetsPath = Assets\bsn\Utils\CommonEx.cs
	// ret bsn\Utils\CommonEx.cs
	public static string PathAssets2AssetsLate(this string strAssetsPath) 
	{
		return strAssetsPath.Substring(ms_assetsPathLength + 1);
	}

	// 返回全路径的Assets路径
	// strFullPath = F:\github\bsnxlua\u3d\Assets\bsn\Utils\CommonEx.cs
	// ret Assets\bsn\Utils\CommonEx.cs
	public static string FullPathToAssetsPath(this string strFullPath) 
	{
		return strFullPath.Substring(ms_rootPathLength);
	}

	// 将路径的目录拆分符格式为平台相关的
	// strPath use system directory separator char Path.DirectorySeparatorChar 
	public static string PathFormat(this string strPath) 
	{
		if (Path.DirectorySeparatorChar == '/')
		{
			return strPath.Replace('\\', '/');
		}
		else 
		{
			return strPath.Replace('/', '\\');
		}
	}

	// 路径的上级目录
	// strPath "Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab" 
	// ret Assets/_Game/Resources/Packages/UI
	public static string PathUpDir(this string strPath) 
	{
		return Path.GetDirectoryName(strPath);
	}

	// 路径的扩展名 
	// strPath "Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab" 
	// ret prefab
	public static string PathExtension(this string strPath) 
	{
		return Path.GetExtension(strPath);
	}

	// 路径的带扩展名的文件名 
	// strPath "Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab" 
	// ret EquipAvartarTip.prefab
	public static string PathExtensionFileName(this string strPath) 
	{
		return Path.GetFileName(strPath);
	}

	// 路径的不带扩展名的文件名 
	// strPath "Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab" 
	// ret EquipAvartarTip
	public static string PathNoExtensionFileName(this string strPath) 
	{
		return Path.GetFileNameWithoutExtension(strPath);
	}


	// 路径连接 
	// strPath1 "Assets" 
	// strPath2 "_Game/Resources/Packages/UI/EquipAvartarTip.prefab" 
	// return "Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab" 
	public static string PathCombine(this string strPath1, string strPath2) 
	{
		return Path.Combine(strPath1, strPath2);
	}

	// Assets路径strPath下的相对目录
	// 返回Assets路径的Assets后去除文件的路径 (Assets/*/? => *)
    public static string PathAssetsRelativeDir(this string strPath) 
    {
		var strUpDir = strPath.PathUpDir();
        var strAfter = strUpDir.Substring(ms_assetsPathLength + 1);
        return strAfter;
    }

	public static int ms_assetsPathLength = "Assets".Length;
	public static int ms_dataPathLength = Application.dataPath.Length;
	public static int ms_rootPathLength = ms_dataPathLength - ms_assetsPathLength;

	public static void PathTest() 
	{
		var strDataPath = Application.dataPath;
		Debug.LogFormat("strDataPath={0}", strDataPath);

		var strRelativePath = "Assets"
			.PathCombine("bsn")
			.PathCombine("Utils")
			.PathCombine("CommonEx.cs")
		;
		Debug.LogFormat("strRelativePath={0}", strRelativePath);
	}
}  

}