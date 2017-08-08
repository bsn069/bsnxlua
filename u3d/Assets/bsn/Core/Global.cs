using UnityEngine;
using System.Collections;
// using LuaInterface;
using System.Runtime.InteropServices;
using System;
using XLua;

namespace NBsn {

public class CGlobal : IDisposable 
{
	public static NBsn.CGlobal Instance 
	{
		get { 
			#if UNITY_EDITOR
			if (m_instance == null) 
			{
				new CGlobal();
			}
			#endif
			return m_instance; 
		}
	}

	public NBsn.CLog Log 
	{
		get { return m_Log; }
	}

	public NBsn.CLua Lua
	{
		get { return m_Lua; }
	}

	public NBsn.CResMgr ResMgr
	{
		get { return m_ResMgr; }
	}

	public NBsn.CCoroutine Coroutine 
	{
		get {return m_Coroutine;}
	}

	public NBsn.MMain Main 
	{
		get { return m_Main; }
	}

	public UnityEngine.GameObject GoMain 
	{
		get { return m_goMain; }
	}

	public UnityEngine.Transform TfMain 
	{
		get { return m_tfMain; }
	}

    public event System.Actio OnUpdate;
    public event System.Action OnLateUpdate;

	#region updater
	public void Update()
	{
		OnUpdate();
	}

	public void LateUpdate()
	{
		OnLateUpdate();
	}
	#endregion

	#region game init
	// 游戏逻辑初始化
	public void Init(GameObject goMain, NBsn.MMain Main) 
	{
		m_goMain    = goMain;
		m_Main      = Main;
		m_tfMain    = m_goMain.transform;

		m_Log = new NBsn.CLog();
		Log.Init();

		NBsn.PathConfig.Init();

		m_Coroutine = new NBsn.CCoroutine();
		Coroutine.Init(m_Main);

		m_ResMgr = new NBsn.CResMgr();
		ResMgr.Init();

		m_Lua	= new NBsn.CLua();
		Lua.Init();
	}

	public void UnInit() 
	{
		Log.Info("NBsn.CGlobal.UnInit()"); 

		Lua.UnInit();
		m_Lua = null;

		ResMgr.UnInit();
		m_ResMgr = null;		

		Coroutine.UnInit();	
		m_Coroutine = null;

		Log.UnInit();
		m_Log = null;	

		m_tfMain    = null;
		m_Main      = null;
		m_goMain    = null;
	}
	#endregion

	#region
	public CGlobal() 
	{
		m_instance = this;
	}

	public void Dispose() 
	{
		m_instance = null;
	}

	protected static CGlobal m_instance = null;

	protected NBsn.CLog 		m_Log 		= null;
	protected NBsn.CCoroutine 	m_Coroutine = null;
	protected NBsn.CResMgr 		m_ResMgr 	= null;

	protected NBsn.MMain 	m_Main 		= null;
	protected GameObject 	m_goMain 	= null;
	protected Transform 	m_tfMain 	= null;

	protected NBsn.CLua 	m_Lua 		= null;
	#endregion
}

}