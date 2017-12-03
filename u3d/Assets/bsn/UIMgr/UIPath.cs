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

	public string GetUIPath() {
		return null;
	}

	private Dictionary<string, string> m_name2Path = new Dictionary<string, string>();
}

}