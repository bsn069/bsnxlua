using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

namespace NBsn 
{

public static class C_PathConfig 
{
	// 从服务器下载的资源存放根目录名
	public static string ServerResDirName
	{
		get { return "server_res"; }
	}

    // lua文件根目录名
	public static string LuaDirName
	{
		get { return "lua"; }
	}

    // Assets目录下的lua目录路径
	// server_res/lua
    public static string AssetsLuaDirPath
	{
		get { return m_strAssetsLuaDirPath; }
	}
    static string m_strAssetsLuaDirPath = ServerResDirName.PathCombine(LuaDirName).Unique(false);

    // 版本文件名
	public static string VerFileName
	{
		get { return "ver.bin"; }
	}

    // 非编辑器server资源路径
	// pc C:/Users/butao/AppData/LocalLow/DefaultCompany/bsnxlua/server_res 
    public static string ServerResPath
	{
		get { return m_strServerResPath; }
	}
 	static string m_strServerResPath = 
		Application.persistentDataPath.PathFormat()
		.PathCombine(ServerResDirName)
		.Unique(false);

	public static string AssetsDir
	{
		get { return "m_strAssetsDir"; }
	}
	private static string m_strAssetsDir = "Assets";

	// 编辑器此目录下的文件 会被打成ab
	// app的所有ab存放在小写的此目录
	public static string ABResDir
	{
		get { return m_strABResDir; }
	}
	private static string m_strABResDir = "ABRes";
		

    // ServerResDirName下各平台下ab资源http目录名
	// pc assets/abres 
    public static string ServerResABResHttpDirName
	{
		get { return m_strServerResABResHttpDirName; }
	}
 	static string m_strServerResABResHttpDirName = 
			string.Format("{0}/{1}", AssetsDir, ABResDir)
			.ToLower()
			.Unique(false);

	// ServerResDirName下ab资源http目录
	// pc win/assets/abres 
    public static string ServerResABResHttpDir
	{
		get { return m_strServerResABResHttpDir; }
	}
 	static string m_strServerResABResHttpDir = 
			string.Format("{0}/{1}", NBsn.C_Platform.Name().ToLower(), ServerResABResHttpDirName)
			.Unique(false);














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

    // AB输出Assets下的目录
    // strPlatform="win" server_res/win
    public static string AssetsLatePlatformABOutPath(string strPlatform)
    {
        return ServerResDirName.PathCombine(strPlatform);
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