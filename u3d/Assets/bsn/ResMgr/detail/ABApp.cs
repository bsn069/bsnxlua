
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class CABApp : IABLoad
{
	public GameObject Load(string strPath, string strSuffix) 
	{
		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.CABApp.Load({0}, {1})", strPath, strSuffix); 
		return null;
	}

	public bool Init() {
		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.CABApp.Init()"); 

		return true;
	}

	public void UnInit() {

	}
}

}

