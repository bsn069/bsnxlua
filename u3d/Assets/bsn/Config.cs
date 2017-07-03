using UnityEngine;
using System;
using System.Collections.Generic;

namespace NBsn 
{

public static class Config 
{
	// AB存放相对Assets目录格式化
	// {0} strPlatform[Win, Android]
	public static string PlatformABPathFormat 
	{
		get { return "ABOut/{0}/AB"; }
	}

	// 资源加载类型
	public static NBsn.EResLoadType ResLoadType 
	{
		get { return ms_eResLoadType; }
		set { ms_eResLoadType = value; }
	}

	public static string ServerResLocalDirName
	{
		get { return "ServerRes"; }
	}

	#region
	private static NBsn.EResLoadType ms_eResLoadType = NBsn.EResLoadType.EditorABRes;
	#endregion
}

}