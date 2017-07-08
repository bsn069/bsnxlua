using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class CResources : I_GameObjectLoad, I_Init
{
	public GameObject Load(S_GameObjectLoadParam p)
	{
		return Load(p.strPath);
	}

	public bool Init() 
	{
		return true;
	}

	public void UnInit() 
	{
		
	}

	public GameObject Load(string strPath) 
	{
		NBsn.CGlobal.Instance.Log.InfoFormat("NBsn.CResources.Load({0})", strPath); 

		var go = Resources.Load<GameObject>(strPath);
		return go;
	}
}

}