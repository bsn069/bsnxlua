using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;

namespace NBsn 
{

public static class C_Platform 
{
	public static void Info()
	{
#if UNITY_EDITOR
		Debug.Log("UNITY_EDITOR"); 	
#endif
#if UNITY_STANDALONE_OSX
		Debug.Log("UNITY_STANDALONE_OSX"); 	
#endif
#if UNITY_DASHBOARD_WIDGET
		Debug.Log("UNITY_DASHBOARD_WIDGET"); 	
#endif
#if UNITY_STANDALONE_WIN
		Debug.Log("UNITY_STANDALONE_WIN"); 	
#endif
#if UNITY_STANDALONE_LINUX
		Debug.Log("UNITY_STANDALONE_LINUX"); 	
#endif
#if UNITY_STANDALONE
		Debug.Log("UNITY_STANDALONE"); 	
#endif
#if UNITY_WII
		Debug.Log("UNITY_WII"); 	
#endif
#if UNITY_IPHONE
		Debug.Log("UNITY_IPHONE"); 	
#endif
#if UNITY_ANDROID
		Debug.Log("UNITY_ANDROID"); 	
#endif
#if UNITY_PS3
		Debug.Log("UNITY_PS3"); 	
#endif
#if UNITY_XBOX360
		Debug.Log("UNITY_XBOX360"); 	
#endif
#if UNITY_NACL
		Debug.Log("UNITY_NACL"); 	
#endif
#if UNITY_FLASH
		Debug.Log("UNITY_FLASH"); 	
#endif
	}

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
		"win"
		, "android"
	};
} 

}