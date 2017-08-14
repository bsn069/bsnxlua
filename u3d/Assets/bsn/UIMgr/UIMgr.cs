using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class C_UIMgr: I_Init
{
	public bool Init() 
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.C_UIMgr.Init()");
		return true;
	}

	public void UnInit() 
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.C_UIMgr.UnInit()");
	}
}

}