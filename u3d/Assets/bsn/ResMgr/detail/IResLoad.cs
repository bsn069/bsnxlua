using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class C_ResLoadParam
{
	public string strPath 	= null;
	public string strSuffix = null;
}

public interface I_ResLoad
{
	/*
	p.strPath = "Prefab/UI/UIBsnUpdate"
	p.strSuffix = "prefab"
	find from path:
		1. form config type dir
			. NBsn.C_Config.ResLoadType = NBsn.E_ResLoadType.EditorABRes
				pc Assets/ABRes/Prefab/UI/UIBsnUpdate.prefab
			. NBsn.C_Config.ResLoadType = NBsn.E_ResLoadType.EditorABOut
				pc Assets/ABOut/Win/AB/prefab/ui/uibsnupdate.prefab.ab
			. NBsn.C_Config.ResLoadType = NBsn.E_ResLoadType.AppAB
				 
		2. not found  
			?Resources/Prefab/UI/UIBsnUpdate.prefab
	*/
	T Load<T>(C_ResLoadParam p) where T : UnityEngine.Object; 
}

}

