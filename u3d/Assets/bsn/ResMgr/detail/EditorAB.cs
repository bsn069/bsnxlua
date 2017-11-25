#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class C_EditorAB
{
	public bool Init() 
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.C_EditorAB.Init()");
		return true;
	}

    public bool InitAfterUpdateRes() 
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_EditorAB.InitAfterUpdateRes()");
		return true;
	}

	public void UnInit() 
	{
		
	}
}

}
#endif
