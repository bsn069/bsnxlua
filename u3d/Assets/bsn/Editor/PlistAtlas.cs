
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml.Linq;

namespace NBsn.NEditor 
{

public static class C_PlistAtlas 
{
    [MenuItem("GameObject/Bsn/PlistAtlas/修改为Multiple", false, 1)]
	[MenuItem("Assets/Bsn/PlistAtlas/修改为Multiple")]
    [MenuItem("Bsn/PlistAtlas/修改为Multiple")]
	static void ChangeToMultiple() 
    {
        string strAssetPath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
		ms_cPlist.SetPlistPath(strAssetPath);
        ms_cPlist.ChangeToMultiple();
    }

    [MenuItem("GameObject/Bsn/PlistAtlas/修改为Multiple", true)]
	[MenuItem("Assets/Bsn/PlistAtlas/修改为Multiple", true)]
    [MenuItem("Bsn/PlistAtlas/修改为Multiple", true)]
    static bool SelectObjValidate() {
       string strAssetPath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
       Debug.LogFormat(strAssetPath);
       string strExtension = Path.GetExtension(strAssetPath);
       return (strExtension.CompareTo(".plist") == 0);
    }

	private static NBsn.NEditor.C_PList ms_cPlist = new NBsn.NEditor.C_PList();
}

}
