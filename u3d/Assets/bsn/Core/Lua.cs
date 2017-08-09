using UnityEngine;
using System.Collections;
// using LuaInterface;
using System.Runtime.InteropServices;
using System;
using XLua;

namespace NBsn {

public class C_Lua 
{
	public object[] DoString(string strLua) 
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_Lua.DoString({0})", strLua); 
		return m_Lua.DoString(strLua);
	}

	#region init
	public void Init() 
	{
		m_Lua = new LuaEnv();
	}

	public void UnInit() 
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_Lua.UnInit()");
		m_Lua.Dispose();
		m_Lua = null;
	}
	#endregion

	protected LuaEnv m_Lua = null;
}

}