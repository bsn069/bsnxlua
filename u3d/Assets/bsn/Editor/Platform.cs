using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using UnityEditor;


namespace NBsn {

public static class CPlatform_Editor {
	public static string Name(BuildTarget buildTarget) {
		Debug.LogFormat("NBsn.CPlatform.Name({0})", buildTarget);
		string strPlatform = "";
		switch (buildTarget) {
			case BuildTarget.Android: {
					strPlatform = "Android";
				} 
				break;               
			case BuildTarget.StandaloneWindows:
			case BuildTarget.StandaloneWindows64: {
					strPlatform = "Win";
				}
				break;              
			default: {
					Debug.LogErrorFormat("unknown buildTarget={0}", buildTarget);
					return null;
				}
		}

		return strPlatform;
	} 
} 

}