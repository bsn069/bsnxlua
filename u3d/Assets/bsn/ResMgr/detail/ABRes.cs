#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;


namespace NBsn 
{

public class C_ABRes: I_ResLoad, I_Init
{
	public T Load<T>(C_ResLoadParam p) where T : UnityEngine.Object
	{
		return Load<T>(p.strPath, p.strSuffix);
	}

	public bool Init() 
	{
		NBsn.C_Global.Instance.Log.InfoFormat("mc_strPathFormat={0}", mc_strPathFormat);
		return true;
	}

	public void UnInit() 
	{
		
	}

	public T Load<T>(string strPath, string strSuffix) where T : UnityEngine.Object
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ABRes.Load({0}, {1})", strPath, strSuffix); 

		var strAssetsPath = string.Format(mc_strPathFormat, strPath, strSuffix);
		NBsn.C_Global.Instance.Log.InfoFormat("strAssetsPath={0}", strAssetsPath);
		return AssetDatabase.LoadAssetAtPath<T>(strAssetsPath);
	}

	private readonly string mc_strPathFormat = NBsn.C_PathConfig.AssetsABResPath.PathCombine("{0}.{1}");
}

}
#endif