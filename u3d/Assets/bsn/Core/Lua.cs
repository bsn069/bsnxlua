using UnityEngine;
using System.Collections;
// using LuaInterface;
using System.Runtime.InteropServices;
using System;
using XLua;

namespace NBsn {

public class CLua 
{
	public object[] DoString(string strLua) 
	{
		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.CLua.DoString({0})", strLua); 
		return m_Lua.DoString(strLua);
	}

	#region init
	public void Init() 
	{
		m_Lua = new LuaEnv();
	}

	public void UnInit() 
	{
		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.CLua.UnInit()");
		m_Lua.Dispose();
		m_Lua = null;
	}
	#endregion

	protected LuaEnv m_Lua = null;
}

}