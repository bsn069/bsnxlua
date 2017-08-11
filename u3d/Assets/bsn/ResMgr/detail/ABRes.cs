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
		var strAssetsPath = string.Format(mc_strPathFormat, p.strPath, p.strSuffix);
		return AssetDatabase.LoadAssetAtPath<T>(strAssetsPath);
	}

	public UnityEngine.Object[] LoadAll(C_ResLoadParam p)
	{
		var strAssetsPath = string.Format(mc_strPathFormat, p.strPath, p.strSuffix);
		return AssetDatabase.LoadAllAssetsAtPath(strAssetsPath);
	}

	public bool Init() 
	{
		NBsn.C_Global.Instance.Log.InfoFormat("mc_strPathFormat={0}", mc_strPathFormat);
		return true;
	}

	public void UnInit() 
	{
		
	}

	private readonly string mc_strPathFormat = NBsn.C_PathConfig.AssetsABResPath.PathCombine("{0}.{1}");
}

}
#endif