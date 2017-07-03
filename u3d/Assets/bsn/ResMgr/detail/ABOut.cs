#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class CABOut : IABLoad 
{
	public GameObject Load(string strPath, string strSuffix) 
	{
		return null;
	}

	public bool Init() 
	{
		return true;
	}

	public void UnInit() 
	{
		
	}
}

}
#endif
