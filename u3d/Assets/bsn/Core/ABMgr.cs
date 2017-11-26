
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
	/*
	strAssetPath Assets下的路径
		Resources/Prefab/UI/UIUpdate.prefab
	 */
	public T LoadAsset<T>(string strAssetPath)  where T : UnityEngine.Object
	{
		NBsn.C_Global.Instance.Log.InfoFormat(
			"NBsn.C_ABMgr.LoadAsset<{1}>({0})}"
			, strAssetPath
			, typeof(T)
		);
		
		AssetBundle ab = GetABByAssetPath(strAssetPath);
		if (ab == null) 
        {
			NBsn.C_Global.Instance.Log.ErrorFormat(
				"NBsn.C_ABMgr.LoadAsset<{1}>({0}) not found ab"
				, strAssetPath
				, typeof(T)
			);
			return null;
		}

		var strAssetName = strAssetPath.Substring(strAssetPath.LastIndexOf('/'));
		return ab.LoadAsset<T>(strAssetName);
	}

	public UnityEngine.Object[] LoadAll(string strAssetPath)
	{
		NBsn.C_Global.Instance.Log.InfoFormat(
			"NBsn.C_ABMgr.LoadAll({0})}"
			, strAssetPath
		);
		
		AssetBundle ab = GetABByAssetPath(strAssetPath);
		if (ab == null) 
        {
			NBsn.C_Global.Instance.Log.ErrorFormat(
				"NBsn.C_ABMgr.LoadAll({0}) not found ab"
				, strAssetPath
			);
			return null;
		}

		var strAssetName = strAssetPath.Substring(strAssetPath.LastIndexOf('/'));
		return ab.LoadAllAssets();
	}

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

		foreach (var item in m_abCache)
		{
			item.Value.Unload(false);
		}
		m_abCache.Clear();

		m_abManifest = null;
		m_ABResRootPath = null;
	}

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
		ab = null;

		return true;
	}

	/*
	strAssetPath Assets下的路径
		Resources/Prefab/UI/UIUpdate.prefab
	ret 
		not found return 0
	 */
	public UInt32 GetABIdByAssetPath(string strAssetPath)
	{
		UInt32 u32ABId;
		if (!m_mapRes2ABId.TryGetValue(strAssetPath, out u32ABId))
		{
			NBsn.C_Global.Instance.Log.InfoFormat(
				"NBsn.C_ABMgr.GetABIdByAssetPath({0}) not found"
				, strAssetPath
			);
			return 0;
		}
		return u32ABId;
	}

	/*
	strAssetPath Assets下的路径
		Resources/Prefab/UI/UIUpdate.prefab
	 */
	public AssetBundle GetABByAssetPath(string strAssetPath)
	{
		NBsn.C_Global.Instance.Log.InfoFormat(
			"NBsn.C_ABMgr.GetABByAssetPath({0})"
			, strAssetPath
		);
		
		UInt32 u32ABId = GetABIdByAssetPath(strAssetPath);
		if (u32ABId == 0)
		{
			NBsn.C_Global.Instance.Log.ErrorFormat(
				"NBsn.C_ABMgr.GetABByAssetPath({0})"
				, strAssetPath
			);
			return null;
		}

		AssetBundle ab = GetABById(u32ABId);
		if (ab == null) 
        {
			NBsn.C_Global.Instance.Log.ErrorFormat(
				"NBsn.C_ABMgr.GetABByAssetPath({0}) not found u32ABId={1}"
				, strAssetPath
				, u32ABId
			);
			return null;
		}

		return ab;
	}

	public AssetBundle GetABById(UInt32 u32ABId) 
    {
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ABMgr.GetABById({0})", u32ABId);

		if (m_abManifest == null)
		{
			NBsn.C_Global.Instance.Log.ErrorFormat("not m_abManifest u32ABId={0}", u32ABId);
			return null;
		}

		AssetBundle ab;
		if (m_abCache.TryGetValue(u32ABId, out ab)) 
        {
			return ab;
		}
		NBsn.C_Global.Instance.Log.InfoFormat("load ab u32ABId={0}", u32ABId);

		var strABName = string.Format("{0}{1}", u32ABId, NBsn.C_PathConfig.AssetBundleExtName);
		var arrDepd = m_abManifest.GetDirectDependencies(strABName);
		for(int i = 0; i < arrDepd.Length; ++i) 
		{
			var strDepABPath = arrDepd[i];
			NBsn.C_Global.Instance.Log.InfoFormat("i={0} strDepABPath={1}", i, strDepABPath);
			var strDepABId = strDepABPath.Substring(0, strDepABPath.Length - NBsn.C_PathConfig.AssetBundleExtName.Length);
			UInt32 u32DepABId;
			if (!UInt32.TryParse(strDepABId, out u32DepABId))
			{
				NBsn.C_Global.Instance.Log.ErrorFormat("i={0} strDepABPath={1}", i, strDepABPath);
				continue;
			}
			GetABById(u32DepABId);
		}

		var strGetABByIdPath = m_ABResRootPath.PathCombine(strABName);
		NBsn.C_Global.Instance.Log.InfoFormat("strGetABByIdPath={0}", strGetABByIdPath);
		ab = AssetBundle.LoadFromFile(strGetABByIdPath);
		if (ab == null)
		{
			NBsn.C_Global.Instance.Log.ErrorFormat("not AssetBundel file strGetABByIdPath={0}", strGetABByIdPath);
			return null;
		}
		m_abCache.Add(u32ABId, ab);

		return ab;
	}

    public bool LoadRes2ABId() 
	{
		m_mapRes2ABId.Clear();
		m_mapRes2ABId.Add("Resources/Prefab/UI/UIUpdate.prefab", 1);
		m_mapRes2ABId.Add("ABRes/Prefab/UI/UILuaConsole.prefab", 1);
		m_mapRes2ABId.Add("ABRes/Atlas/common.png", 2);
		m_mapRes2ABId.Add("ABRes/Atlas/uipack.png", 2);

		return true;
	}

	#region 
	private Dictionary<UInt32, AssetBundle> m_abCache = new Dictionary<UInt32, AssetBundle>();
	private Dictionary<string, UInt32> 		m_mapRes2ABId = new Dictionary<string, UInt32>();
	private AssetBundleManifest             m_abManifest = null;
	private string 							m_ABResRootPath = null;
	#endregion
}

}

