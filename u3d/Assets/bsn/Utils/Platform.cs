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
			return "Win";
		#elif UNITY_ANDROID
			return "Android";
		#else
			return null;
		#endif
	} 
} 

}