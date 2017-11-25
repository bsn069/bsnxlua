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
		get { return m_strAssetsDir; }
	}
	static string m_strAssetsDir = "Assets";

	// 从服务器下载的资源存放目录名
	public static string ServerResDirName
	{
		get { return m_strServerResDirName; }
	}
	static string m_strServerResDirName = "server_res";

    // lua文件目录名
	public static string LuaDirName
	{
		get { return "lua"; }
	}
	static string m_strLuaDirName = "lua";

	// ab文件目录名
	public static string ABResDirName
	{
		get { return m_strABResDirName; }
	}
	static string m_strABResDirName = "abres";

	// 平台目录名
	// pc win	
    public static string PlatformDirName
	{
		get { return m_strPlatformDirName; }
	}
 	static string m_strPlatformDirName = NBsn.C_Platform.Name().Unique(false);

    // 版本文件名
	public static string VerFileName
	{
		get { return "ver.bin"; }
	}
	static string m_strVerFileName = "ver.bin";

	// 编辑器中Assets全路径 Application.dataPath
	// E:/github/bsnxlua/u3d/Assets
    public static string EditorAssetsFullPath
	{
		get { return m_strEditorAssetsFullPath; }
	}
    static string m_strEditorAssetsFullPath = Application.dataPath.PathFormat().Unique(false);

	// 持久化数据全路径 Application.persistentDataPath
	// C:/Users/Administrator/AppData/LocalLow/DefaultCompany/bsnxlua
    public static string PersistentDataFullPath
	{
		get { return m_strPersistentDataFullPath; }
	}
    static string m_strPersistentDataFullPath = Application.persistentDataPath.PathFormat().Unique(false);

   	// Assets目录下的lua目录路径
	// server_res/lua
    public static string AssetsLuaDirPath
	{
		get { return m_strAssetsLuaDirPath; }
	}
    static string m_strAssetsLuaDirPath = ServerResDirName.PathCombine(LuaDirName).Unique(false);

   	// Assets目录下的平台目录路径
	// pc server_res/win
    public static string AssetsPlatformDirPath
	{
		get { return m_strAssetsPlatformDirPath; }
	}
 	static string m_strAssetsPlatformDirPath = ServerResDirName.PathCombine(PlatformDirName).Unique(false);

	// Assets目录下的ab资源目录路径
	// pc server_res/win/abres
    public static string AssetsABResDirPath
	{
		get { return m_strAssetsABResDirPath; }
	}
 	static string m_strAssetsABResDirPath = AssetsPlatformDirPath.PathCombine(ABResDirName).Unique(false);

	public static void Debug() 
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.CPathConfig.Debug()"); 

		NBsn.C_Global.Instance.Log.InfoFormat("Application.persistentDataPath={0}", Application.persistentDataPath);
		NBsn.C_Global.Instance.Log.InfoFormat("Application.dataPath={0}", Application.dataPath);
		NBsn.C_Global.Instance.Log.InfoFormat("Application.streamingAssetsPath={0}", Application.streamingAssetsPath);
		NBsn.C_Global.Instance.Log.InfoFormat("Application.temporaryCachePath={0}", Application.temporaryCachePath);

		NBsn.C_Global.Instance.Log.InfoFormat("EditorAssetsFullPath={0}", EditorAssetsFullPath);
		NBsn.C_Global.Instance.Log.InfoFormat("PersistentDataFullPath={0}", PersistentDataFullPath);

		NBsn.C_Global.Instance.Log.InfoFormat("AssetsPlatformDirPath={0}", AssetsPlatformDirPath);
		NBsn.C_Global.Instance.Log.InfoFormat("AssetsABResDirPath={0}", AssetsABResDirPath);

		NBsn.C_Global.Instance.Log.InfoFormat("AssetsLuaDirPath={0}", AssetsLuaDirPath);
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



	// 编辑器此目录下的文件 会被打成ab
	public static string ABResDir
	{
		get { return m_strABResDir; }
	}
	private static string m_strABResDir = "ABRes";
		
	// app的所有ab存放在ServerResDirName下的此目录
	public static string APPABResDir
	{
		get { return m_strAPPABResDir; }
	}
	private static string m_strAPPABResDir = "abres";

	// 非编辑器ab资源根路径
	// pc C:/Users/butao/AppData/LocalLow/DefaultCompany/bsnxlua/server_res/abres 
	public static string APPABResRootPath
	{
		get { return m_strAPPABResRootPath; }
	}
	private static string m_strAPPABResRootPath = NBsn.C_PathConfig.ServerResPath
		.PathCombine(NBsn.C_PathConfig.APPABResDir)
		.Unique(false);

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

	// ABRes下的存放prefab的目录
	public static string ABResPrefabDir
	{
		get { return "Prefab"; }
	}


	/////////////////////////////////////////////////////////////


































	// ABRes下的存放图集的目录
	public static string ABResAtlasDir
	{
		get { return "Atlas"; }
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


}

}