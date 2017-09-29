using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class C_Resources : I_ResLoad, I_Init
{
	public T Load<T>(C_ResLoadParam p) where T : UnityEngine.Object
	{
		NBsn.C_Global.Instance.Log.InfoFormat(
            "NBsn.C_Resources.Load(p.strPath={0})"
            , p.strPath
        ); 
		return Load<T>(p.strPath);
	}

	public UnityEngine.Object[] LoadAll(C_ResLoadParam p)
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

	public T Load<T>(string strPath) where T : UnityEngine.Object
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_Resources.Load({0})", strPath); 

		var go = Resources.Load<T>(strPath);
		return go;
	}
}

}