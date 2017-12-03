using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn {

public class C_UIPath {	
	public bool Init() {
		m_name2Path.Add("UIUpdate", "Resources/Prefab/UI/UIUpdate.prefab");
		return true;
	}

	/*
	strUIName UILogin
	*/
	public string GetUIPath(string strUIName) {
		string strUIPath;
		if (m_name2Path.TryGetValue(strUIName, out strUIPath)) {
			return strUIPath;
		}
		return null;
	}

	private Dictionary<string, string> m_name2Path = new Dictionary<string, string>();
}

}