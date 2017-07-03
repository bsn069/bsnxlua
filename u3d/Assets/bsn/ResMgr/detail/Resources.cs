using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class CResources 
{
	public GameObject Load(string strPath) 
	{
		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.CResources.Load({0})", strPath); 

		var go = Resources.Load<GameObject>(strPath);
		return go;
	}
}

}