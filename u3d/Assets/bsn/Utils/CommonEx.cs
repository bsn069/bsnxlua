using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace NBsn {

public static class CommonEx  
{
	public static string UTF8String(this byte[] me)
	{
        string val = System.Text.Encoding.UTF8.GetString(me);
        return val;
	}

	// 返回全路径的Assets路径
    // path ["H:/dev/swordm3d-code/trunk/client/swordm3d/Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab", ...]
    // ret ["Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab", ...]
    public static List<string> FullPaths2AssetsPaths(this List<string> listPath) 
    {
        List<string> ret = new List<string>();
        foreach (var item in listPath) {
            ret.Add(item.FullPathToAssetsPath());
        }
        return ret;
    }

	// Instantiate go then set name
	public static GameObject Clone(this GameObject go)
	{
		var goClone = (GameObject)UnityEngine.Object.Instantiate(go);
		goClone.name = goClone.name.Replace("(Clone)", "");
		return goClone;
	}
}  

}