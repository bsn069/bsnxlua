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

	// 将strPath中的cDirectorySeparatorChar替换为目录拆分符
	public static string PathReplaceToDirectorySeparatorChar(this string strPath, char cDirectorySeparatorChar) 
	{
		return strPath.Replace(cDirectorySeparatorChar, Path.DirectorySeparatorChar);
	}

	// 路径的上级目录
	// strPath "Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab" 
	// ret Assets/_Game/Resources/Packages/UI
	public static string PathUpDir(this string strPath) 
	{
		return Path.GetDirectoryName(strPath);
	}

	/* 路径的上级目录名
	strPath "Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab" 
	ret UI
	*/
	public static string PathUpDirName(this string strPath) 
	{
		var strUpDir = strPath.PathUpDir();
		return Path.GetFileName(strUpDir);
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

	// 移除左边的strTrim字符串
	// strPath "Assets/ABRes/Prefab/UI/EquipAvartarTip.prefab" 
	// strTrim "Assets/ABRes/"
	// ret Prefab/UI/EquipAvartarTip.prefab
	public static string TrimLeftString(this string strPath, string strTrim) 
	{
		if (strPath.StartsWith(strTrim))
		{
			return strPath.Substring(strTrim.Length);
		}
		return strPath;
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

	#region Unique string
	//  
    public static string Unique(this string str, bool bCanRemove = true) 
    {
        if (str == null)
            return null;

        string ret = IsUnique(str);
        if (ret != null)
            return ret;

        if (bCanRemove) {
            // the app-level interning (which could be cleared regularly)
            ms_uniqueStrings.Add(str, str);
            return str;
        } else {
            return string.Intern(str);
        }
    }

	//  
    public static string IsUnique(this string str) 
    {
        if (str == null)
            return null;

        string ret = string.IsInterned(str);
        if (ret != null)
            return ret;

        if (ms_uniqueStrings.TryGetValue(str, out ret))
            return ret;

        return null;
    }

	//  
    public static void UniqueClear() 
    {
        ms_uniqueStrings.Clear();
    }

	// Why use Dictionary? 
    //  http://stackoverflow.com/questions/7760364/how-to-retrieve-actual-item-from-hashsett
    private static Dictionary<string, string> ms_uniqueStrings = new Dictionary<string, string>();
	#endregion

	#region int
	public static string Int(int iNumber) 
    {
       	string ret = null;
        if (ms_intStrings.TryGetValue(iNumber, out ret)) {
            return ret;
        }
        ret = iNumber.ToString();
        ms_intStrings.Add(iNumber, ret);
        return ret;
    }

    public static void IntClear() 
	{
		ms_intStrings.Clear();
	}

    private static NBsn.NContainer.Map<int, string> ms_intStrings = new NBsn.NContainer.Map<int, string>();	
	#endregion

	#region
	public static bool ToInt(this string str, int defaultValue, out int resultValue)
	{
        bool bResult = int.TryParse(str, out resultValue);
        if (bResult == false)
        {
            resultValue = defaultValue;
            Log.Error("failed to ToInt: {0}", str);
        }
        return bResult;
	}

	public static bool ToByte(string s, byte defaultValue, out byte resultValue) 
    {
        bool bResult = byte.TryParse(s, out resultValue);
        if (bResult == false) 
        {
            resultValue = defaultValue;
            Log.Error("failed to ToByte: {0}", s);
        }

        return bResult;
    }

    public static bool ToUint(string s, uint defaultValue, out uint resultValue)
    {
        bool bResult = uint.TryParse(s, out resultValue);
        if (bResult == false)
        {
            resultValue = defaultValue;
            Log.Error("failed to ToUint: {0}", s);
        }

        return bResult;
    }

    public static bool ToFloat(string s, float defaultValue, out float resultValue)
    {
        bool bResult = float.TryParse(s, out resultValue);
        if (bResult == false)
        {
            resultValue = defaultValue;
            Log.Error("failed to ToFloat: {0}", s);
        }

        return bResult;
    }

    public static bool ToDouble(string s, double defaultValue, out double resultValue) {
        bool bResult = double.TryParse(s, out resultValue);
        if (bResult == false) {
            resultValue = defaultValue;
            Log.Error("failed to ToDouble: {0}", s);
        }

        return bResult;
    }

    public static bool ToBool(string s, bool defaultValue, out bool resultValue)
    {
        bool bResult = bool.TryParse(s, out resultValue);
        if (bResult == false)
        {
            resultValue = defaultValue;
            Log.Error("failed to ToBool: {0}", s);
        }

        return bResult;
    }

    public static bool ToEnum<T>(string s, bool bIngnoreCase, T defaultValue, out T resultValue)
    {
        bool bResult = true;
        resultValue = defaultValue;
        try
        {
            resultValue = (T)Enum.Parse(typeof(T), s, bIngnoreCase);
        }
        catch
        {
            bResult = false;
            Log.Error("failed to ToEnum: {0}", s);
        }

        return bResult;
    }

    public static bool ToInt64(string s, Int64 defaultValue, out Int64 resultValue)
    {
        bool bResult = Int64.TryParse(s, out resultValue);
        if (bResult == false)
        {
            resultValue = defaultValue;
            Log.Error("failed to ToInt64: {0}", s);
        }

        return bResult;
    }

    public static bool ToUint64(string s, UInt64 defaultValue, out UInt64 resultValue)
    {
        bool bResult = UInt64.TryParse(s, out resultValue);
        if (bResult == false)
        {
            resultValue = defaultValue;
            Log.Error("failed to ToUint64: {0}", s);
        }

        return bResult;
    }

	public static bool ToNumber<T>(string s, T defaultValue, out T resultValue)
    {
		bool bResult = T.TryParse(s, out resultValue);
        if (bResult == false)
        {
            resultValue = defaultValue;
            Log.Error("failed to ToNumber<{1}>: {0}", s, typeof(T));
        }

        return bResult;
    }
	#endregion
}  

}