using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class CResMgr: I_GameObjectLoad, I_Init
{
	public  GameObject Load(S_GameObjectLoadParam p)
	{
		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.CResMgr.Load({0})", p);

		// first from ab
		GameObject go = m_iGameObjectLoad.Load(p);
		if (go == null) {
			// second from resources
			go = m_Resources.Load(p);
		}

		if (go != null) {
			go = (GameObject)UnityEngine.Object.Instantiate(go);
			go.name = go.name.Replace("(Clone)", "");
		}
		return go;
	}

	public  bool Init() 
	{
		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.CResMgr.Init()");

		m_Resources = new NBsn.CResources();
		var iInit = m_Resources as I_Init;
		if (!iInit.Init())
		{
			return false;
		}

		switch (NBsn.Config.ResLoadType) {
#if UNITY_EDITOR
			case NBsn.EResLoadType.EditorABRes: {
				m_iGameObjectLoad = new NBsn.CABRes();
			}
			break;
			case NBsn.EResLoadType.EditorABOut: {
				m_iGameObjectLoad = new NBsn.CABOut();
			}
			break;
#endif
			case NBsn.EResLoadType.AppAB: {
				m_iGameObjectLoad = new NBsn.CABApp();
			}
			break;
			default: {
				NBsn.CGlobal.Instance.Log.InfoFormat("ResLoadType={0}", NBsn.Config.ResLoadType);
				return false;
			}
		}

		iInit = m_iGameObjectLoad as I_Init;
		return iInit.Init();
	}

	public  void UnInit() 
	{
		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.CResMgr.UnInit()");
		if (m_iGameObjectLoad != null)
		{
			var iInit = m_iGameObjectLoad as I_Init;
			iInit.UnInit();
			m_iGameObjectLoad = null;
		}

		if (m_Resources != null)
		{
			var iInit = m_Resources as I_Init;
			iInit.UnInit();
			m_Resources = null;
		}
	}

	protected NBsn.CResources		m_Resources 		= new NBsn.CResources();
	protected NBsn.I_GameObjectLoad	m_iGameObjectLoad 	= null;
}

}