using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn {

public class C_UIMgr {	
	/*
	strUIName UILogin
	*/
	public NBsn.NMVVM.I_View GetView(string strUIName) {
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_UIMgr.GetView() strUIName={0}", strUIName);

		GameObject pGo = GetUI(strUIName);
		if (pGo == null) {
			return null;
		}

		return pGo.GetComponent<NBsn.NMVVM.I_View>();
	}

    /*
	strUIName UILogin
	*/
	public NBsn.NMVVM.ViewModel GetVM(string strUIName)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_UIMgr.GetView() strUIName={0}", strUIName);

		var pView = GetView(strUIName);
		if (pView == null)
		{
			return null;
		}
        return pView.GetVM();
	}

	/*
	strUIName UILogin
	*/
	public GameObject GetUI(string strUIName)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_UIMgr.GetUI() strUIName={0}", strUIName);

		GameObject pRet;
		if (!m_mapUIs.TryGetValue(strUIName, out pRet))
		{
			return LoadUI(strUIName);
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

		var canvas = goUIPrefeb.GetComponent<Canvas>();
		canvas.worldCamera = m_Camera;

		m_mapUIs.Add(strUIName, goUIPrefeb);
		goUIPrefeb.SetActive(false);
		return goUIPrefeb;
	}

	#region init
	public bool Init(Transform tfMain) 
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.C_UIMgr.Init()");
		m_tfUI = tfMain.Find("UIMgr");
		m_Camera = tfMain.Find("UICamera").GetComponent<Camera>();
		return true;
	}

	public void UnInit() 
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.C_UIMgr.UnInit()");

		var itor = m_mapUIs.GetEnumerator();
        while (itor.MoveNext())
        {
            GameObject.DestroyImmediate(itor.Current.Value);
        }
		m_mapUIs.Clear();
	}
	#endregion

	private Transform 	m_tfUI = null;
	private Camera 		m_Camera = null;
	private string 		m_strPathSuffix = NBsn.C_PathConfig.ABResPrefabDir + "/UI/";
	private NBsn.NContainer.Map<string, GameObject> m_mapUIs = new NBsn.NContainer.Map<string, GameObject>();
}

}