using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

namespace NBsn 
{

public static class PathConfig 
{
	// AB根目录 相对资源路径 
	// pc ABOut/Win/AB
	public static string PlatformABPath 
	{
		get { return m_strPlatformABPath; }
	}

	// AB根目录 本地全路径
	// pc F:/github/bsnxlua/u3d/Assets/ABOut/Win/AB/
	public static string ABLocalFullPath 
	{
		get { return m_strABLocalFullPath; }
	}

	// 资源根目录 本地全路径
	// pc F:/github/bsnxlua/u3d/Assets/
	public static string ResLocalFullPath 
	{
		get { return m_strResLocalFullPath; }
	}

	public static void Init() 
	{
		NBsn.CGlobal.Instance.Log.Info("NBsn.CPathConfig.Init()"); 

		NBsn.CGlobal.Instance.Log.InfoFormat("Application.persistentDataPath={0}", Application.persistentDataPath);
		NBsn.CGlobal.Instance.Log.InfoFormat("Application.dataPath={0}", Application.dataPath);
		NBsn.CGlobal.Instance.Log.InfoFormat("Application.streamingAssetsPath={0}", Application.streamingAssetsPath);
		NBsn.CGlobal.Instance.Log.InfoFormat("Application.temporaryCachePath={0}", Application.temporaryCachePath);

		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.Config.PlatformABPathFormat={0}", NBsn.Config.PlatformABPathFormat); 
		m_strPlatformABPath = string.Format(
			NBsn.Config.PlatformABPathFormat
			, NBsn.CPlatform.Name()
			);
		NBsn.CGlobal.Instance.Log.InfoFormat("PlatformABPath={0}", PlatformABPath); 

		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.Config.ResLoadType={0}", NBsn.Config.ResLoadType); 
		if (NBsn.Config.ResLoadType == NBsn.EResLoadType.AppAB) 
		{
			m_strResLocalFullPath   = Application.persistentDataPath + "/" + NBsn.Config.ServerResLocalDirName + "/";
		}
		else 
		{
			m_strResLocalFullPath   = Application.dataPath + "/";
		}
		NBsn.CGlobal.Instance.Log.InfoFormat("ResLocalFullPath={0}", ResLocalFullPath);

		m_strABLocalFullPath    = ResLocalFullPath + PlatformABPath + "/";
		NBsn.CGlobal.Instance.Log.InfoFormat("ABLocalFullPath={0}", ABLocalFullPath);
	}


	#region
	private static string    m_strPlatformABPath = null;
	private static string    m_strABLocalFullPath = null;
	private static string    m_strResLocalFullPath = null;
	#endregion
}

}