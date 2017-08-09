using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;

namespace NBsn {

public static class CPlatform 
{

	public static string Name() 
	{
		#if UNITY_STANDALONE
			return GetName(0);
		#elif UNITY_ANDROID
			return GetName(1);
		#else
			return null;
		#endif
	} 

	public static string GetName(uint index)
	{
		return ms_platformNames[index];
	} 

	private static string[] ms_platformNames = new string[]
	{
		"Win"
		, "Android"
	};
} 

}