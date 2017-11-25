using UnityEngine;
using System.Collections;
// using LuaInterface;
using System.Runtime.InteropServices;
using System;
using XLua;

#if UNITY_EDITOR
using UnityEditor;
#else
using System.IO;
#endif

namespace NBsn {

public class C_Lua 
{
	public void Get<TKey, TValue>(TKey key, out TValue value)
	{
		m_G.Get(key, out value);
	}

	public LuaTable NewTable()
	{
		return m_Lua.NewTable();
	}

	public object[] DoString(string strLua) 
	{
		try
		{
			NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_Lua.DoString({0})", strLua); 
			return m_Lua.DoString(strLua);	
		}
		catch (LuaException e)
		{
			NBsn.C_Global.Instance.Log.Error(e); 
			return null;
		}
	}

	#region init
	public void Init() 
	{
		m_Lua = new LuaEnv();
		m_G = m_Lua.Global;
		m_Lua.AddLoader(Require);
		NBsn.C_Global.Instance.EventMgr.Add((int)E_EventId.Global_LateUpdate, LateUpdate);
	}

	public void UnInit() 
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_Lua.UnInit()");
		NBsn.C_Global.Instance.EventMgr.Del((int)E_EventId.Global_LateUpdate, LateUpdate);
		m_Lua.Dispose();
		m_Lua = null;
	}
	#endregion

	private void LateUpdate()
	{
		if (m_Lua != null)
		{
			m_Lua.Tick();
		}
	}

	/*
	strRequireParam x.yy.zzz
	 */
	private byte[] Require(ref string strRequireParam)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("Require({0})", strRequireParam);

		var strRequirePath = strRequireParam.PathReplaceToDirectorySeparatorChar('.');
		NBsn.C_Global.Instance.Log.InfoFormat("strRequirePath={0}", strRequirePath);

		var strRequirePathFile = string.Format("{0}.txt", strRequirePath);
		var strFilePath = mc_strPathRoot.PathCombine(strRequirePathFile);

#if UNITY_EDITOR
		TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(strFilePath);
		if (textAsset == null) {
			NBsn.C_Global.Instance.Log.ErrorFormat("strFilePath={0}", strFilePath);
            return null;
        }
		return textAsset.bytes;
#else
		StreamReader sr = new StreamReader(strFilePath);
		return sr.ReadToEnd().UTF8Bytes();
#endif
	}

	protected LuaEnv m_Lua = null;
	protected LuaTable m_G = null;
	// lua文件根目录
#if UNITY_EDITOR
	protected readonly string mc_strPathRoot = 
		NBsn.C_PathConfig.AssetsDir
		.PathCombine(NBsn.C_PathConfig.AssetsLuaDirPath)
		.Unique(false);
#else
	protected readonly string mc_strPathRoot = 
		NBsn.C_PathConfig.PersistentDataFullPath
		.PathCombine(NBsn.C_PathConfig.AssetsLuaDirPath)
		.Unique(false);
#endif
}

}