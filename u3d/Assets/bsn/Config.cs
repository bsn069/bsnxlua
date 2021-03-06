﻿using UnityEngine;
using System;
using System.Collections.Generic;

namespace NBsn 
{

public static class C_Config 
{
	// 资源加载类型
	public static NBsn.E_ResLoadType ResLoadType 
	{
		get { return ms_eResLoadType; }
	}

	// AB文件后缀
	public static string ABSuffix
	{
		get { return ".ab";}
	}

	// Lua文件后缀
	public static string LuaSuffix
	{
		get { return ".txt";}
	}

	// ServerResDirName在http服务器的根目录 /结尾
	public static string ServerResHttpUrl
	{
		get { return "http://localhost:10001/";}
	}


	#region
	private static NBsn.E_ResLoadType ms_eResLoadType = 
#if !UNITY_EDITOR
        NBsn.E_ResLoadType.AppAB;
#else
        NBsn.E_ResLoadType.EditorRes;
#endif
	#endregion
}

}