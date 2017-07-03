using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class CResMgr
{
	public GameObject Load(string strPath, string strSuffix) 
	{
		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.CResMgr.Load({0}, {1})", strPath, strSuffix);

		GameObject go = m_iABLoad.Load(strPath, strSuffix);
		if (go == null) {
			go = m_Resources.Load(strPath);
		}

		if (go != null) {
			go = (GameObject)UnityEngine.Object.Instantiate(go);
			go.name = go.name.Replace("(Clone)", "");
		}
		return go;
	}

	public bool Init() {
		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.CResMgr.Init()");

		switch (NBsn.Config.ResLoadType) {
#if UNITY_EDITOR
			case NBsn.EResLoadType.EditorABRes: {
				m_iABLoad = new NBsn.CABRes();
			}
			break;
			case NBsn.EResLoadType.EditorABOut: {
				m_iABLoad = new NBsn.CABOut();
			}
			break;
#endif
			case NBsn.EResLoadType.AppAB: {
				m_iABLoad = new NBsn.CABApp();
			}
			break;
			default: {
				NBsn.CGlobal.Instance.Log.InfoFormat("ResLoadType={0}", NBsn.Config.ResLoadType);
				return false;
			}
		}

		return m_iABLoad.Init();
	}

	public void UnInit() {
		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.CResMgr.UnInit()");
		m_iABLoad.UnInit();
		m_iABLoad = null;
	}

	protected NBsn.CResources    m_Resources = new NBsn.CResources();
	protected NBsn.IABLoad 	m_iABLoad = null;
}

}