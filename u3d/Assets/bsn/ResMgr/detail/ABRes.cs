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

public class CABRes: I_GameObjectLoad, I_Init
{
	public GameObject Load(S_GameObjectLoadParam p)
	{
		return Load(p.strPath, p.strSuffix);
	}

	public bool Init() 
	{
		return true;
	}

	public void UnInit() 
	{
		
	}

	public GameObject Load(string strPath, string strSuffix) 
	{
		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.CABRes.Load({0}, {1})", strPath, strSuffix); 

		strPath = string.Format(mc_strPathFormat, strPath, strSuffix);
		NBsn.CGlobal.Instance.Log.InfoFormat("strPath={0}", strPath);
		return AssetDatabase.LoadAssetAtPath<GameObject>(strPath);
	}

	private const string mc_strPathFormat = "Assets/ABRes/{0}.{1}";
}

}
#endif