using UnityEngine;
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
		set { ms_eResLoadType = value; }
	}

	// AB文件后缀
	public static string ABSuffix
	{
		get { return ".ab";}
	}

	#region
	private static NBsn.E_ResLoadType ms_eResLoadType = NBsn.E_ResLoadType.EditorABRes;
	#endregion
}

}