#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class C_ABOut : I_ResLoad, I_Init
{
	public T Load<T>(C_ResLoadParam p)  where T : UnityEngine.Object
	{
		var strABResLowerName = GetABResLowerName(p);
		AssetBundle ab = LoadAB(strABResLowerName);
		if (ab == null) 
		{
			return null;
		}
 
		return ab.LoadAsset<T>(strABResLowerName);
	}

	public UnityEngine.Object[] LoadAll(C_ResLoadParam p)
	{
		var strABResLowerName = GetABResLowerName(p);
		AssetBundle ab = LoadAB(strABResLowerName);
		if (ab == null) 
		{
			return null;
		}

		return ab.LoadAllAssets();
	}

	/*
	ret assets\abres\atlas\common.png
	ret assets\abres\atlas\red.prefab
	 */
	private string GetABResLowerName(C_ResLoadParam p)
	{
		return NBsn.C_PathConfig.AssetsABResPath.PathCombine(p.strPath + "." + p.strSuffix).ToLower();
	}

	public bool Init() 
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.C_ABOut.Init()");

		if (NBsn.C_Config.ResLoadType == NBsn.E_ResLoadType.EditorABOut) 
		{
			var strPlatformName = NBsn.C_Platform.Name();
			m_strABResRootPath = Application.dataPath.PathCombine(NBsn.C_PathConfig.AssetsLatePlatformABOutPath(strPlatformName)).PathFormat();
		}
		NBsn.C_Global.Instance.Log.InfoFormat("m_strABResRootPath={0}", m_strABResRootPath);
 
		var strLoadPath = m_strABResRootPath.PathCombine(NBsn.C_PathConfig.ABRootDir);
		var ab = AssetBundle.LoadFromFile(strLoadPath);
		m_abManifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
		ab.Unload(false);
		return true;
	}

	public void UnInit() 
	{
		
	}

	/*
	strABResLowerName assets\abres\atlas\common.png
	strABResLowerName assets\abres\atlas\red.prefab
	 */
	private AssetBundle LoadAB(string strABResLowerName) {
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ABOut.LoadAB()strABResLowerName={0}", strABResLowerName);

		AssetBundle ab;
		if (!m_abCache.TryGetValue(strABResLowerName, out ab)) {
			var strABName =strABResLowerName + NBsn.C_Config.ABSuffix;
			NBsn.C_Global.Instance.Log.InfoFormat("strABName={0}", strABName);

			var arrDepd = m_abManifest.GetDirectDependencies(strABName);
			for(int i = 0; i < arrDepd.Length; ++i) {
				var strDepABName = arrDepd[i];
				NBsn.C_Global.Instance.Log.InfoFormat("i={1} strDepABName={0}", strDepABName, i);
				strDepABName = strDepABName.Substring(0, strDepABName.Length - 3);
				LoadAB(strDepABName.PathFormat());
			}

			var strLoadPath = m_strABResRootPath.PathCombine(strABName);
			NBsn.C_Global.Instance.Log.InfoFormat("strLoadPath={0}", strLoadPath);
			ab = AssetBundle.LoadFromFile(strLoadPath);
			if (ab == null) 
			{
				NBsn.C_Global.Instance.Log.Error("ab == null");
			}
			else
			{
				m_abCache.Add(strABResLowerName, ab);
			}
		}
		return ab;
	}

	private Dictionary<string, AssetBundle> m_abCache = new Dictionary<string, AssetBundle>();
	private AssetBundleManifest             m_abManifest = null;
	// win F:\github\bsnxlua\u3d\Assets\ABOut\Win\AB
	private string m_strABResRootPath = null;
}

}
#endif
