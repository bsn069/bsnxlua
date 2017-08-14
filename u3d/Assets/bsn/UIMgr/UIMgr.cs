using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class C_UIMgr
{
	/*
	strUIName UILogin
	*/
	public GameObject GetUI(string strUIName, bool bLoad)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_UIMgr.GetUI() strUIName={0}", strUIName);

		GameObject pRet;
		if (!m_mapUIs.TryGetValue(strUIName, out pRet))
		{
			if (bLoad)
			{
				return LoadUI(strUIName);
			}
		}
		return pRet;
	}

	/*
	strUIName UILogin
	*/
	private GameObject LoadUI(string strUIName)
	{
		C_ResLoadParam pResLoadParam = new C_ResLoadParam();
		pResLoadParam.strPath = m_strPathSuffix + strUIName;
		pResLoadParam.strSuffix = "prefab";
		pResLoadParam.m_bClone = true;

		var goUIPrefeb = NBsn.C_Global.Instance.ResMgr.Load<GameObject>(pResLoadParam);
		if (goUIPrefeb == null)
		{
			return null;
		}

		goUIPrefeb.transform.SetParent(m_tfUI);
		var rectTransform = goUIPrefeb.GetComponent<RectTransform>();
		rectTransform.localPosition = Vector3.zero;
		rectTransform.sizeDelta = Vector2.zero;
		rectTransform.localScale = Vector3.one;

		m_mapUIs.Add(strUIName, goUIPrefeb);
		return goUIPrefeb;
	}

	#region init
	public bool Init(Transform tfUIMgr) 
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.C_UIMgr.Init()");
		m_tfUI = tfUIMgr;
		// GetUI("UILogin", true);
		// GetUI("UILogin", true);
		return true;
	}

	public void UnInit() 
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.C_UIMgr.UnInit()");
	}
	#endregion

	private Transform 	m_tfUI = null;
	private string 		m_strPathSuffix = "Prefab".PathCombine("UI") + Path.DirectorySeparatorChar;
	private NBsn.NContainer.Map<string, GameObject> m_mapUIs = new NBsn.NContainer.Map<string, GameObject>();
}

}