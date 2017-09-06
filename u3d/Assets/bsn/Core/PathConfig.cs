using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

namespace NBsn 
{

public static class C_PathConfig 
{
	public static string AssetsDir
	{
		get { return "Assets"; }
	}

	// 此目录下的文件 会被打包
	public static string ABResDir
	{
		get { return "ABRes"; }
	}

	// ABRes下的存放图集的目录
	public static string ABResAtlasDir
	{
		get { return "Atlas"; }
	}

	// ABRes下的存放prefab的目录
	public static string ABResPrefabDir
	{
		get { return "Prefab"; }
	}

	// ABRes下的存放lua的目录
	public static string ABResLuaDir
	{
		get { return "Lua"; }
	}

	// ab资源存放目录
	// win Assets/ABRes
	public static string AssetsABResPath
	{
		get { return AssetsDir.PathCombine(ABResDir); }
	}

	// AB根目录名
	public static string ABRootDir
	{
		get { return "AB"; }
	}

	// AB输出Assets下的目录
	// strPlatform="Win" ABOut/Win/AB
	public static string AssetsLatePlatformABOutPath(string strPlatform) 
	{
		return "ABOut".PathCombine(strPlatform).PathCombine(ABRootDir);
	}

	// Assets上层全路径
	// pc F:/github/bsnxlua/u3d
	public static string AssetsUpFullPath() 
	{
		return Application.dataPath.PathUpDir().PathFormat();
	}








	// AB根目录 本地全路径
	// pc F:/github/bsnxlua/u3d/Assets/ABOut/Win/AB/
	public static string ABLocalFullPath 
	{
		get { return m_strABLocalFullPath; }
	}

	// 资源根目录 本地全路径
	// pc editor F:/github/bsnxlua/u3d/Assets/
	public static string ResLocalFullPath 
	{
		get { return m_strResLocalFullPath; }
	}

	public static void Init() 
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.CPathConfig.Init()"); 

		NBsn.C_Global.Instance.Log.InfoFormat("Application.persistentDataPath={0}", Application.persistentDataPath);
		NBsn.C_Global.Instance.Log.InfoFormat("Application.dataPath={0}", Application.dataPath);
		NBsn.C_Global.Instance.Log.InfoFormat("Application.streamingAssetsPath={0}", Application.streamingAssetsPath);
		NBsn.C_Global.Instance.Log.InfoFormat("Application.temporaryCachePath={0}", Application.temporaryCachePath);



		// NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_Config.PlatformABPathFormat={0}", NBsn.C_Config.PlatformABPathFormat); 
		// m_strPlatformABPath = string.Format(
		// 	NBsn.C_Config.PlatformABPathFormat
		// 	, NBsn.CPlatform.Name()
		// 	);

		// NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_Config.ResLoadType={0}", NBsn.C_Config.ResLoadType); 
		// if (NBsn.C_Config.ResLoadType == NBsn.E_ResLoadType.AppAB) 
		// {
		// 	// m_strResLocalFullPath   = Application.persistentDataPath + "/" + NBsn.C_Config.ServerResLocalDirName + "/";
		// }
		// else 
		// {
		// 	m_strResLocalFullPath   = Application.dataPath + "/";
		// }
		// NBsn.C_Global.Instance.Log.InfoFormat("ResLocalFullPath={0}", ResLocalFullPath);
 
		// NBsn.C_Global.Instance.Log.InfoFormat("ABLocalFullPath={0}", ABLocalFullPath);
	}


	#region
    //private static string	m_strPlatformABPath = null;
	private static string	m_strABLocalFullPath = null;
	private static string	m_strResLocalFullPath = null;

	#endregion


}

}