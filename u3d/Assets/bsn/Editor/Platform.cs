using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using UnityEditor;


namespace NBsn.NEditor
{

public static class CPlatform
{
	public static string Name(BuildTarget buildTarget) 
	{
		Debug.LogFormat("NBsn.NEditor.CPlatform.Name({0})", buildTarget);
		string strPlatform = "";
		switch (buildTarget) 
		{
			case BuildTarget.Android: 
				{
					strPlatform = NBsn.C_Platform.GetName(1);
				} 
				break;               
			case BuildTarget.StandaloneWindows:
			case BuildTarget.StandaloneWindows64: 
				{
					strPlatform = NBsn.C_Platform.GetName(0);
				}
				break;              
			default: 
				{
					Debug.LogErrorFormat("unknown buildTarget={0}", buildTarget);
					return null;
				}
		}

		return strPlatform;
	} 
} 

}