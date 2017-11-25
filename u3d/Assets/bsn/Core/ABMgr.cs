
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class C_ABMgr
{
	public bool Init(string strABResRootPath)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ABMgr.Init({0})", strABResRootPath); 
		if (strABResRootPath == null)
		{
			string strRootPath;
			#if UNITY_EDITOR
				strRootPath = NBsn.C_PathConfig.EditorAssetsFullPath;
			#else
				strRootPath = NBsn.C_PathConfig.PersistentDataFullPath;
			#endif
			m_ABResRootPath = strRootPath.PathCombine(NBsn.C_PathConfig.AssetsABResDirPath).Unique(false);
		}
		else 
		{
			m_ABResRootPath = strABResRootPath.Unique(false);
		}
		NBsn.C_Global.Instance.Log.InfoFormat("m_ABResRootPath={0}", m_ABResRootPath); 
		return true;
	}

	public void UnInit() 
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.C_ABMgr.UnInit()"); 
		m_abCache.Clear();
		m_abManifest = null;
		m_ABResRootPath = null;
	}

	/*
	desc
		from AssetBundle(strABPath) load asset(strAssetPath)
	param
		strABPath lower ab path
			xx/xx/xx.ab
		strAssetPath lower want load asset path
			prefab/ui/uiupdate.prefab
	 */
	public T LoadAsset<T>(string strABPath, string strAssetPath)  where T : UnityEngine.Object
	{
		NBsn.C_Global.Instance.Log.InfoFormat(
			"NBsn.C_ABMgr.LoadAsset<{2}>({0}, {1})"
			, strABPath
			, strAssetPath
			, 1
		);
		
		AssetBundle ab = GetAB(strABPath);
		if (ab == null) 
        {
			NBsn.C_Global.Instance.Log.ErrorFormat(
				"NBsn.C_ABMgr.LoadAsset<{2}>({0}, {1}) not ab"
				, strABPath
				, strAssetPath
				, 1
			);
			return null;
		}

		return ab.LoadAsset<T>(strAssetPath);
	}

	/*
	strABManifestPath lower path
	*/
    public bool LoadABManifest() 
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.C_ABMgr.LoadABManifest()");

        if (m_abManifest != null) 
        {
			NBsn.C_Global.Instance.Log.Info("m_abManifest != null");
            return true;
        }

		var strABManifestPath = m_ABResRootPath.PathCombine(NBsn.C_PathConfig.ABResDirName);
		NBsn.C_Global.Instance.Log.InfoFormat("strABManifestPath={0}", strABManifestPath);
        if (!File.Exists(strABManifestPath)) 
        {
			NBsn.C_Global.Instance.Log.ErrorFormat("file not exist strABManifestPath={0}", strABManifestPath);
            return false;
        }

		var ab = AssetBundle.LoadFromFile(strABManifestPath);
		if (ab == null)
		{
			NBsn.C_Global.Instance.Log.ErrorFormat("not AssetBundel file strABManifestPath={0}", strABManifestPath);
			return false;
		}

		m_abManifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
		if (m_abManifest == null)
		{
			NBsn.C_Global.Instance.Log.ErrorFormat("not AssetBundleManifest strABManifestPath={0}", strABManifestPath);
			return false;
		}
		ab.Unload(false);

		return true;
	}

	// strABPath */*/*.ab
	public AssetBundle GetAB(string strABPath) 
    {
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ABMgr.GetAB({0})", strABPath);

		if (m_abManifest == null)
		{
			NBsn.C_Global.Instance.Log.ErrorFormat("not m_abManifest strABPath={0}", strABPath);
			return null;
		}

		AssetBundle ab;
		if (m_abCache.TryGetValue(strABPath, out ab)) 
        {
			return ab;
		}
		NBsn.C_Global.Instance.Log.InfoFormat("load ab strABPath={0}", strABPath);

		var arrDepd = m_abManifest.GetDirectDependencies(strABPath);
		for(int i = 0; i < arrDepd.Length; ++i) 
		{
			var strDepABPath = arrDepd[i];
			NBsn.C_Global.Instance.Log.InfoFormat("i={1} strDepABPath={0}", strDepABPath, i);
			GetAB(strDepABPath);
		}

		var strGetABPath = m_ABResRootPath.PathCombine(strABPath.PathFormat());
		NBsn.C_Global.Instance.Log.InfoFormat("strGetABPath={0}", strGetABPath);
		ab = AssetBundle.LoadFromFile(strGetABPath);
		if (ab == null)
		{
			NBsn.C_Global.Instance.Log.ErrorFormat("not AssetBundel file strGetABPath={0}", strGetABPath);
			return null;
		}
		m_abCache.Add(strABPath, ab);

		return ab;
	}

	#region 
	private Dictionary<string, AssetBundle> m_abCache = new Dictionary<string, AssetBundle>();
	private AssetBundleManifest             m_abManifest = null;
	private string 							m_ABResRootPath = null;
	#endregion
}

}

