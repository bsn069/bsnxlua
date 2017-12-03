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
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_UIMgr.GetView({0})", strUIName);

		GameObject pGo = GetUI(strUIName);
		if (pGo == null) {
			return null;
		}

		return pGo.GetComponent<NBsn.NMVVM.I_View>();
	}

    /*
	strUIName UILogin
	*/
	public NBsn.NMVVM.ViewModel GetVM(string strUIName)	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_UIMgr.GetVM({0})", strUIName);

		var pView = GetView(strUIName);
		if (pView == null) {
			return null;
		}
        return pView.GetVM();
	}

	/*
	strUIName UILogin
	*/
	private GameObject GetUI(string strUIName) {
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_UIMgr.GetUI({0})", strUIName);

		GameObject pRet;
		if (!m_mapUIs.TryGetValue(strUIName, out pRet))	{
			return pRet;
		}
		return LoadUI(strUIName);
	}

	/*
	strUIName UILogin
	*/
	private GameObject LoadUI(string strUIName)	{
		NBsn.C_Global.Instance.Log.InfoFormat(
			"NBsn.C_UIMgr.LoadUI({0})"
			, strUIName
		);	

		var strUIPath = m_UIPath.GetUIPath(strUIName);
		if (strUIPath == null) {
			NBsn.C_Global.Instance.Log.ErrorFormat(
				"NBsn.C_UIMgr.LoadUI({0}) not found path"
				, strUIName
			);	
			return null;
		}

		var uiPrefab = NBsn.C_Global.Instance.ResMgr.LoadAsset<GameObject>(strUIPath);
		if (uiPrefab == null) {
			NBsn.C_Global.Instance.Log.ErrorFormat(
				"NBsn.C_UIMgr.LoadUI({0}) not found asset {1}"
				, strUIName
				, strUIPath
			);	
			return null;
		}
		var goUIPrefeb = uiPrefab.Clone<GameObject>();

		goUIPrefeb.transform.SetParent(m_tfUI);
		var rectTransform = goUIPrefeb.GetComponent<RectTransform>();
		rectTransform.localPosition = Vector3.zero;
		rectTransform.sizeDelta 	= Vector2.zero;
		rectTransform.localScale 	= Vector3.one;

		var canvas = goUIPrefeb.GetComponent<Canvas>();
		canvas.worldCamera = m_Camera;

		m_mapUIs.Add(strUIName, goUIPrefeb);
		goUIPrefeb.SetActive(false);
		return goUIPrefeb;
	}

	#region init
	public bool Init(Transform tfMain) {
		NBsn.C_Global.Instance.Log.Info("NBsn.C_UIMgr.Init()");
		m_UIPath.Init();
		m_tfUI = tfMain.Find("UIMgr");
		m_Camera = tfMain.Find("UICamera").GetComponent<Camera>();
		return true;
	}

	public void InitAfterUpdateRes() {

	}

	public void UnInit() {
		NBsn.C_Global.Instance.Log.Info("NBsn.C_UIMgr.UnInit()");

		ClearCacheUI();
	}
	#endregion

	public void ClearCacheUI() {
		NBsn.C_Global.Instance.Log.Info("NBsn.C_UIMgr.ClearCacheUI()");

		var itor = m_mapUIs.GetEnumerator();
        while (itor.MoveNext()) {
            GameObject.DestroyImmediate(itor.Current.Value);
        }
		m_mapUIs.Clear();
	}

	private Transform 	m_tfUI = null;
	private Camera 		m_Camera = null;
	private NBsn.NContainer.Map<string, GameObject> m_mapUIs = new NBsn.NContainer.Map<string, GameObject>();
	private C_UIPath 	m_UIPath = new C_UIPath();
}

}