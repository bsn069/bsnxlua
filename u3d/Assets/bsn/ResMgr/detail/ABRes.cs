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

public class CABRes : IABLoad
{
	public GameObject Load(string strPath, string strSuffix) 
	{
		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.CABRes.Load({0}, {1})", strPath, strSuffix); 

		strPath = string.Format(m_strPathFormat, strPath, strSuffix);
		NBsn.CGlobal.Instance.Log.InfoFormat("strPath={0}", strPath);
		return AssetDatabase.LoadAssetAtPath<GameObject>(strPath);
	}

	public bool Init() 
	{
		return true;
	}

	public void UnInit() 
	{

	}

	private string m_strPathFormat = "Assets/ABRes/{0}.{1}";
}

}
#endif