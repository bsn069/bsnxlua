using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;

namespace NBsn 
{
 
public static class CNetState 
{
	public static bool IsLan() 
	{
		return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
	}

	public static bool IsMobile() 
	{
		return Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork;
	}

	public static bool HaveNet() 
	{
		return Application.internetReachability != NetworkReachability.NotReachable;
	}

	public static string NetName() 
	{
		if (IsLan()) 
		{
			return "lan";
		}

		if (IsMobile()) 
		{
			return "mobile";
		}

		return "NotNetwork";
	}

	public static void DebugPrint() 
	{
		Debug.LogFormat("NetName={0}", NetName());
	}
}  

}