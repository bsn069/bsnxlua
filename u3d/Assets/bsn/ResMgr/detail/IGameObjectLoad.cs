using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;

namespace NBsn 
{

public class S_GameObjectLoadParam
{
	public string strPath 	= null;
	public string strSuffix = null;
}

public interface I_GameObjectLoad
{
	/*
	p.strPath = "Prefab/UI/UIBsnUpdate"
	p.strSuffix = "prefab"
	find from path:
		1. form config type dir
			. NBsn.Config.ResLoadType = NBsn.EResLoadType.EditorABRes
				pc Assets/ABRes/Prefab/UI/UIBsnUpdate.prefab
			. NBsn.Config.ResLoadType = NBsn.EResLoadType.EditorABOut
				pc Assets/ABOut/Win/AB/assets/abres/prefab/ui/uibsnupdate.prefab.ab
			. NBsn.Config.ResLoadType = NBsn.EResLoadType.AppAB
				?pc Assets/ABOut/Win/AB/assets/abres/prefab/ui/uibsnupdate.prefab.ab
		2. not found  
			?Resources/Prefab/UI/UIBsnUpdate.prefab
	*/
	GameObject Load(S_GameObjectLoadParam p); 
}

}

