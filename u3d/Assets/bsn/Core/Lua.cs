using UnityEngine;
using System.Collections;
// using LuaInterface;
using System.Runtime.InteropServices;
using System;
using XLua;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

	/*
	strRequireParam x.yy.zzz
	 */
	private byte[] Require(ref string strRequireParam)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("Require({0})", strRequireParam);

		var strRequirePath = strRequireParam.PathReplaceToDirectorySeparatorChar('.');
		NBsn.C_Global.Instance.Log.InfoFormat("strRequirePath={0}", strRequirePath);

#if UNITY_EDITOR
		var strRequirePathFile = string.Format("{0}.txt", strRequirePath);
		var strFilePath = mc_strPathRoot.PathCombine(strRequirePathFile);
		TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(strFilePath);
		if (textAsset == null) {
			NBsn.C_Global.Instance.Log.ErrorFormat("strFilePath={0}", strFilePath);
            return null;
        }
		return textAsset.bytes;
#else
		return null;
#endif

		// NBsn.C_ResLoadParam pResLoadParam = new NBsn.C_ResLoadParam();
		// pResLoadParam.strSuffix = "txt";
		// pResLoadParam.strPath = NBsn.C_PathConfig.ABResLuaDir.PathCombine(strFilePath);
		// TextAsset textAsset = NBsn.C_Global.Instance.ResMgr.Load<TextAsset>(pResLoadParam);
		// if (textAsset == null) {
        //     return null;
        // }
		// return textAsset.bytes;
	}

	protected LuaEnv m_Lua = null;
	// lua文件根目录
	protected readonly string mc_strPathRoot = NBsn.C_PathConfig.AssetsDir.PathCombine("ServerRes").PathCombine("Lua").Unique(false);
}

}