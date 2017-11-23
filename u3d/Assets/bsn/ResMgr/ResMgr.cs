using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class C_ResMgr: I_ResLoad, I_Init, I_InitAfterUpdateRes
{
	public enum E_ResType
	{
		
	}

	public T Load<T>(C_ResLoadParam p) where T : UnityEngine.Object
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ResMgr.Load({0})", p);

		// first from res
		T ret = m_iResLoad.Load<T>(p);
		if (ret == null) {
			// second from resources
			ret = m_Resources.Load<T>(p);
		}

		if (p.m_bClone && ret != null) {
			GameObject go = ret as GameObject;
			if (go != null)
			{
				return go.Clone<T>();
			}
			return null;
		}

		return ret;
	}

	public UnityEngine.Object[] LoadAll(C_ResLoadParam p)
	{
		var ret = m_iResLoad.LoadAll(p);
		if (ret == null) {
			// second from resources
			ret = m_Resources.LoadAll(p);
		}

		return ret;
	}

	public bool Init() 
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ResMgr.Init()");

		m_Resources = new NBsn.C_Resources();
		var iInit = m_Resources as I_Init;
		if (!iInit.Init())
		{
			return false;
		}

		switch (NBsn.C_Config.ResLoadType) {
#if UNITY_EDITOR
			case NBsn.E_ResLoadType.EditorABRes: {
				m_iResLoad = new NBsn.C_ABRes();
			}
			break;
			case NBsn.E_ResLoadType.EditorABOut: {
				m_iResLoad = new NBsn.C_ABOut();
			}
			break;
#endif
			case NBsn.E_ResLoadType.AppAB: {
				m_iResLoad = new NBsn.C_ABApp();
			}
			break;
			default: {
				NBsn.C_Global.Instance.Log.InfoFormat("ResLoadType={0}", NBsn.C_Config.ResLoadType);
				return false;
			}
		}

		iInit = m_iResLoad as I_Init;
		return iInit.Init();
	}

    public bool InitAfterUpdateRes() 
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ResMgr.InitAfterUpdateRes()");

		var iInit = m_iResLoad as I_InitAfterUpdateRes;
		return iInit.InitAfterUpdateRes();
	}


	public void UnInit() 
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ResMgr.UnInit()");
		if (m_iResLoad != null)
		{
			var iInit = m_iResLoad as I_Init;
			iInit.UnInit();
			m_iResLoad = null;
		}

		if (m_Resources != null)
		{
			var iInit = m_Resources as I_Init;
			iInit.UnInit();
			m_Resources = null;
		}
	}

	protected NBsn.C_Resources	m_Resources	= new NBsn.C_Resources();
	protected NBsn.I_ResLoad	m_iResLoad 	= null;
}

}