using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn {

public class C_AtlasMgr {
	public Sprite GetSprite(string strAtlas, string strSprite) {
		NBsn.C_Global.Instance.Log.InfoFormat(
			"NBsn.C_AtlasMgr.GetSprite({0}, {1})"
			, strAtlas
			, strSprite
		);

		var pAtlas = GetAtlas(strAtlas, true);
		if (pAtlas == null) {
			return null;
		}

		return pAtlas.GetSprite(strSprite);
	}

	private NBsn.C_Atlas GetAtlas(string strAtlas, bool bLoad) {
		NBsn.C_Global.Instance.Log.InfoFormat(
			"NBsn.C_AtlasMgr.GetAtlas({0}, {1})"
			, strAtlas
			, bLoad
		);

		NBsn.C_Atlas pRet;
		if (m_name2Atlas.TryGetValue(strAtlas, out pRet)) {
			return pRet;
		}

		if (bLoad) {
			return LoadAtlas(strAtlas);
		}
		return null;
	}

	public bool Init() {
		NBsn.C_Global.Instance.Log.Info("NBsn.C_AtlasMgr.Init()");
		// GetSprite("common", "zhanchangshuxing_huo");
		return true;
	}

	public void UnInit() {
		NBsn.C_Global.Instance.Log.Info("NBsn.C_AtlasMgr.UnInit()");
	}

	private NBsn.C_Atlas LoadAtlas(string strAtlas)	{
		var pObjectes = NBsn.C_Global.Instance.ResMgr.LoadAll(null);
		if (pObjectes == null) {
			return null;
		}

		NBsn.C_Atlas pAtlas = new NBsn.C_Atlas(strAtlas);		
		foreach (var pObject in pObjectes) {
			if (pObject is Sprite) {
				pAtlas.AddSprite(pObject.name, pObject as Sprite);
			}
		}
		m_name2Atlas.Add(pAtlas.Name, pAtlas);

		return pAtlas;
	}

	private NBsn.NContainer.Map<string, NBsn.C_Atlas> m_name2Atlas = new NBsn.NContainer.Map<string, NBsn.C_Atlas>();
}

}