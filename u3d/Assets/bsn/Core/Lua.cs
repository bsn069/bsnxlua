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
		m_Lua.AddLoader(Require);
		NBsn.C_Global.Instance.EventMgr.Add((int)E_EventId.Global_LateUpdate, Update);
	}

	public void UnInit() 
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_Lua.UnInit()");
		NBsn.C_Global.Instance.EventMgr.Del((int)E_EventId.Global_LateUpdate, Update);
		m_Lua.Dispose();
		m_Lua = null;
	}
	#endregion

	private void Update()
	{
		if (m_Lua != null)
		{
			m_Lua.Tick();
		}
	}

	private byte[] Require(ref string strRequireParam)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("Require({0})", strRequireParam);

		var strFilePath = strRequireParam.PathReplaceToDirectorySeparatorChar('.');

		NBsn.C_ResLoadParam pResLoadParam = new NBsn.C_ResLoadParam();
		pResLoadParam.strSuffix = "txt";
		pResLoadParam.strPath = NBsn.C_PathConfig.ABResLuaDir.PathCombine(strFilePath);
		TextAsset textAsset = NBsn.C_Global.Instance.ResMgr.Load<TextAsset>(pResLoadParam);
		if (textAsset == null) {
            return null;
        }
		return textAsset.bytes;
	}

	protected LuaEnv m_Lua = null;
}

}