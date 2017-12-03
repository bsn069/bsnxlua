using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace NBsn {

public class C_ResMgr {
	/*
	strAssetPath Assets下的路径
		Resources/Prefab/UI/UIUpdate.prefab
	*/
	public T LoadAsset<T>(string strAssetPath)  where T : UnityEngine.Object {
		NBsn.C_Global.Instance.Log.InfoFormat(
			"NBsn.C_ResMgr.LoadAsset<{1}>({0})"
			, strAssetPath
			, typeof(T)
		);

		T ret = null;
		switch (NBsn.C_Config.ResLoadType) {
#if UNITY_EDITOR
			case NBsn.E_ResLoadType.EditorRes: {
				ret = AssetDatabase.LoadAssetAtPath<T>(strAssetPath);
			}
			break;
			case NBsn.E_ResLoadType.EditorAB: {
				ret = NBsn.C_Global.Instance.ABMgr.LoadAsset<T>(strAssetPath);
			}
			break;
#endif
			case NBsn.E_ResLoadType.AppAB: {
				ret = NBsn.C_Global.Instance.ABMgr.LoadAsset<T>(strAssetPath);
			}
			break;
			default: {
				NBsn.C_Global.Instance.Log.ErrorFormat(
					"NBsn.C_ResMgr.LoadAsset<{1}>({0}) ResLoadType={2}"
					, strAssetPath
					, typeof(T)
					, NBsn.C_Config.ResLoadType
				);
			}
			break;
		}

		if (ret == null) {
			if (strAssetPath.StartsWith("Resources/")) {
				var strResPath = strAssetPath.Substring("Resources/".Length);
				ret = Resources.Load<T>(strResPath);
			}

			if (ret == null) {
				NBsn.C_Global.Instance.Log.ErrorFormat(
					"NBsn.C_ResMgr.LoadAsset<{1}>({0}) ResLoadType={2} fail"
					, strAssetPath
					, typeof(T)
					, NBsn.C_Config.ResLoadType
				);	
			}
		}

		return null;
	}
	
	/*
	strAssetPath Assets下的路径
		Resources/Prefab/UI/UIUpdate.prefab
	 */
	public UnityEngine.Object[] LoadAll(string strAssetPath)  {
		NBsn.C_Global.Instance.Log.InfoFormat(
			"NBsn.C_ResMgr.LoadAll({0})"
			, strAssetPath
		);

		UnityEngine.Object[] ret = null;
		switch (NBsn.C_Config.ResLoadType) {
#if UNITY_EDITOR
			case NBsn.E_ResLoadType.EditorRes: {
				ret = AssetDatabase.LoadAllAssetsAtPath(strAssetPath);
			}
			break;
			case NBsn.E_ResLoadType.EditorAB: {
				ret = NBsn.C_Global.Instance.ABMgr.LoadAll(strAssetPath);
			}
			break;
#endif
			case NBsn.E_ResLoadType.AppAB: {
				ret = NBsn.C_Global.Instance.ABMgr.LoadAll(strAssetPath);
			}
			break;
			default: {
				NBsn.C_Global.Instance.Log.ErrorFormat(
					"NBsn.C_ResMgr.LoadAll({0}) ResLoadType={2}"
					, strAssetPath
					, NBsn.C_Config.ResLoadType
				);
			}
			break;
		}

		if (ret == null) {
			if (strAssetPath.StartsWith("Resources/")) {
				var strResPath = strAssetPath.Substring("Resources/".Length);
				ret = Resources.LoadAll(strResPath);
			}

			if (ret == null) {
				NBsn.C_Global.Instance.Log.ErrorFormat(
					"NBsn.C_ResMgr.LoadAll({0}) ResLoadType={1} fail"
					, strAssetPath
					, NBsn.C_Config.ResLoadType
				);	
			}
		}

		return null;
	}
	public bool Init() {
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ResMgr.Init()");

		return true;
	}

    public bool InitAfterUpdateRes() {
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ResMgr.InitAfterUpdateRes()");

		return true;
	}

	public void UnInit() {
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_ResMgr.UnInit()");
	}
}

}