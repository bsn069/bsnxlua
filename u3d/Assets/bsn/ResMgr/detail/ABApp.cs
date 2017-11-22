
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class C_ABApp : I_ResLoad, I_Init, I_InitAfterUpdateRes
{
	public T Load<T>(C_ResLoadParam p)  where T : UnityEngine.Object
	{
        if (m_abManifest == null) 
        {
		    NBsn.C_Global.Instance.Log.Info("NBsn.C_ABApp.Load<T>(C_ResLoadParam p) m_abManifest == null"); 
            return null;
        }

		return Load<T>(p.strPath, p.strSuffix);
	}

	public UnityEngine.Object[] LoadAll(C_ResLoadParam p)
	{
		return null;
	}

	public bool Init() 
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ABApp.Init()"); 
 		return LoadABManifest();
	}

    public bool InitAfterUpdateRes() 
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ABApp.InitAfterUpdateRes()");
		return LoadABManifest();
	}

    private bool LoadABManifest() 
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ABApp.LoadABManifest()");

        if (m_abManifest != null) 
        {
            return true;
        }

		var strABPath = NBsn.C_PathConfig.ServerResPath.PathCombine(NBsn.C_Platform.Name().ToLower());
        if (!File.Exists(strABPath)) 
        {
            return false;
        }

		var ab = AssetBundle.LoadFromFile(strABPath);
		m_abManifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
		ab.Unload(false);
		return true;
	}

	public void UnInit() 
	{
		
	}

	public T Load<T>(string strPath, string strSuffix)  where T : UnityEngine.Object
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ABApp.Load({0}, {1})", strPath, strSuffix); 
		
		var strABPath = (strPath + "." + strSuffix).ToLower() + NBsn.C_Config.ABSuffix;
		AssetBundle ab = LoadAB(strABPath);
		if (ab == null) 
        {
			return null;
		}

		var index = strPath.LastIndexOf('/');
		var strResName = strABPath.Substring(index+1);
		strResName = strResName.Substring(0, strResName.Length - 3);
		NBsn.C_Global.Instance.Log.InfoFormat("strResName={0}", strResName);
		return ab.LoadAsset<T>(strResName);
	}

	#region 
	private Dictionary<string, AssetBundle> m_abCache = new Dictionary<string, AssetBundle>();
	private AssetBundleManifest             m_abManifest = null;
	#endregion

	// strABPath */*/*.ab
	private AssetBundle LoadAB(string strABPath) 
    {
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ABApp.LoadAB({0})", strABPath);

		AssetBundle ab;
		if (!m_abCache.TryGetValue(strABPath, out ab)) 
        {
			var arrDepd = m_abManifest.GetDirectDependencies(strABPath);
			for(int i = 0; i < arrDepd.Length; ++i) 
            {
				var strDepABName = arrDepd[i];
				NBsn.C_Global.Instance.Log.InfoFormat("i={1} strDepABName={0}", strDepABName, i);
				LoadAB(strDepABName);
			}

			var strLoadPath = NBsn.C_PathConfig.APPABResRootPath
				.PathCombine(strABPath.PathFormat());
			NBsn.C_Global.Instance.Log.InfoFormat("strLoadPath={0}", strLoadPath);
			ab = AssetBundle.LoadFromFile(strLoadPath);
			if (ab != null) 
            {
				m_abCache.Add(strABPath, ab);
			}
		}
		return ab;
	}
}

}

