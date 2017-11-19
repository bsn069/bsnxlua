using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using XLua;
using HedgehogTeam.EasyTouch;

namespace NBsn {

public class C_Global : IDisposable 
{
	public static NBsn.C_Global Instance 
	{
		get { return m_instance; }
	}

	#region member
	public NBsn.C_Log Log 
	{
		get { return m_Log; }
	}

	public NBsn.C_EventMgr EventMgr
	{
		get { return m_EventMgr; }
	}

	public NBsn.C_Lua Lua
	{
		get { return m_Lua; }
	}

	public NBsn.C_AtlasMgr AtlasMgr
	{
		get { return m_AtlasMgr; }
	}

	public NBsn.C_ResMgr ResMgr
	{
		get { return m_ResMgr; }
	}

	public NBsn.C_UIMgr UIMgr
	{
		get { return m_UIMgr; }
	}

	public NBsn.C_Coroutine Coroutine 
	{
		get {return m_Coroutine;}
	}

	public NBsn.M_Main Main 
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

	public NBsn.C_Gesture LuaConsoleGesture 
	{
		get { return m_LuaConsoleGesture; }
	}
	#endregion

#if UNITY_EDITOR
	public static void EditorInit()
	{
		var pGlobal = NBsn.C_Global.Instance;
		if (pGlobal != null)
		{
			return;
		}
		new NBsn.C_Global();
		pGlobal = NBsn.C_Global.Instance;
		pGlobal.Init(null, null);
	}

	public static void EditorUnInit()
	{
		var pGlobal = NBsn.C_Global.Instance;
		if (pGlobal == null)
		{
			return;
		}
		pGlobal.UnInit();
		pGlobal.Dispose();
	}
#endif

	#region updater
	public void Update()
	{
		EventMgr.Exec((int)E_EventId.Global_Update);
	}

	public void LateUpdate()
	{
		EventMgr.Exec((int)E_EventId.Global_LateUpdate);
	}
	#endregion

	#region game init
	// 游戏逻辑初始化
	public void Init(GameObject goMain, NBsn.M_Main Main) 
	{
		NBsn.C_Platform.Info();
		if (goMain != null)
		{
			m_goMain    = goMain;
			m_Main      = Main;
			m_tfMain    = m_goMain.transform;
		}

		m_Log = new NBsn.C_Log();
		Log.Init();

		m_EventMgr = new NBsn.C_EventMgr();		
		EventMgr.Init();

		NBsn.C_PathConfig.Init();

		if (m_Main != null)
		{
			m_Coroutine = new NBsn.C_Coroutine();
			Coroutine.Init(m_Main);
		}

		m_ResMgr = new NBsn.C_ResMgr();
		ResMgr.Init();

		m_AtlasMgr = new NBsn.C_AtlasMgr();
		AtlasMgr.Init();

		if (m_tfMain != null)
		{
			m_UIMgr = new NBsn.C_UIMgr();
			UIMgr.Init(m_tfMain);
		}

        m_Lua	= new NBsn.C_Lua();
        Lua.Init();

		m_LuaConsoleGesture = new NBsn.C_Gesture();
		LuaConsoleGesture.Init(
			()=>{
				var pView = UIMgr.GetView("UILuaConsole") as NBsn.NMVVM.LuaConsoleV;
        		pView.Show();	
			}
			, EasyTouch.SwipeDirection.Right
			, EasyTouch.SwipeDirection.Down
			, EasyTouch.SwipeDirection.Left
			, EasyTouch.SwipeDirection.Up
		);

		Coroutine.Start(StartApp());
        // var pView = UIMgr.GetView("UIUpdate") as NBsn.NMVVM.UpdateV;
        // pView.Show();
        // //var pVM = pView.VM;
        // //pVM.TextCenter.Value = "";

        // var pUpdateRes = new NBsn.NUpdateRes.C_UpdateRes();
        // pUpdateRes.GetVerInfo();
        //Lua.DoString("require('main')");


		// C_ResLoadParam pResLoadParam = new C_ResLoadParam();
		// pResLoadParam.strPath = @"atlas\red";
		// pResLoadParam.strSuffix = "prefab";
		// var a = ResMgr.Load<Sprite>(pResLoadParam);
		// Log.Info(a); 
	}

	public void UnInit() 
	{
		Log.Info("NBsn.C_Global.UnInit()"); 

		LuaConsoleGesture.UnInit();
		m_LuaConsoleGesture = null;
		
		Lua.UnInit();
		m_Lua = null;

		if	(UIMgr != null)
		{
			UIMgr.UnInit();
			m_UIMgr = null;		
		}

		AtlasMgr.UnInit();
		m_AtlasMgr = null;		

		if (ResMgr != null)
		{
			ResMgr.UnInit();
			m_ResMgr = null;		
		}

		if (Coroutine != null)
		{
			Coroutine.UnInit();	
			m_Coroutine = null;
		}

		EventMgr.UnInit();
		m_EventMgr = null;

		Log.UnInit();
		m_Log = null;	

		m_tfMain    = null;
		m_Main      = null;
		m_goMain    = null;
	}
	#endregion

	private IEnumerator StartApp()
    {
		var pView = UIMgr.GetView("UIUpdate") as NBsn.NMVVM.UpdateV;
        pView.Show();

        var pUpdateRes = new NBsn.NUpdateRes.C_UpdateRes();
		yield return Coroutine.Start(pUpdateRes.Run());
#if UNITY_EDITOR
		if (pUpdateRes.IsSuccess())
		{
			yield break;
		}
#endif
        ResMgr.InitAfterUpdateRes();

        Lua.DoString("require('main')");
	}

	#region
	public C_Global() 
	{
		m_instance = this;
	}

	public void Dispose() 
	{
		m_instance = null;
	}

	protected static C_Global m_instance = null;

	protected NBsn.C_Log 		m_Log 		= null;
	protected NBsn.C_Coroutine 	m_Coroutine = null;
	protected NBsn.C_AtlasMgr	m_AtlasMgr 	= null;
	protected NBsn.C_ResMgr		m_ResMgr 	= null;
	protected NBsn.C_UIMgr		m_UIMgr 	= null;
	protected NBsn.C_EventMgr	m_EventMgr 	= null;

	protected NBsn.M_Main 	m_Main 		= null;
	protected GameObject 	m_goMain 	= null;
	protected Transform 	m_tfMain 	= null;

	protected NBsn.C_Lua 	m_Lua 		= null;

	protected NBsn.C_Gesture m_LuaConsoleGesture	= null;
	#endregion
}

}