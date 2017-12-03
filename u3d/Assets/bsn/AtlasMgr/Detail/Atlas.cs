using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn {

public class C_Atlas {	
	public string Name 	{
		get { return m_strName; }
	}

	public Sprite GetSprite(string strSpriteName) {
		Sprite ret = null;
		m_mapSprites.TryGetValue(strSpriteName, out ret);
		return ret;
	}

    public C_Atlas(string strName) {
		m_strName = strName;
    }

	public void AddSprite(string strSpriteName, Sprite pSprite) {
		// NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_Atlas.AddSprite({0}, {1})", strSpriteName, pSprite);

		m_mapSprites.Add(strSpriteName, pSprite);
	}

	private string m_strName = null;
	private NBsn.NContainer.Map<string, Sprite> m_mapSprites = new NBsn.NContainer.Map<string, Sprite>();
}

}