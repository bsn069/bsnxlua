
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
    [MenuItem("GameObject/Bsn/PlistAtlas/拆解", false, 48)]
    [MenuItem("Assets/Bsn/PlistAtlas/拆解")]
    [MenuItem("Bsn/PlistAtlas/拆解")]
    static void SelectObj() 
    {
        string strAssetPath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
        ms_cPlist.LoadFile(strAssetPath);
    }

    [MenuItem("GameObject/Bsn/PlistAtlas/拆解", true)]
    [MenuItem("Assets/Bsn/PlistAtlas/拆解", true)]
    [MenuItem("Bsn/PlistAtlas/拆解", true)]
    static bool SelectObjValidate() {
       string strAssetPath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
       Debug.LogFormat(strAssetPath);
       string strExtension = Path.GetExtension(strAssetPath);
       return (strExtension.CompareTo(".plist") == 0);
    }

	private static NBsn.NEditor.C_PList ms_cPlist = new NBsn.NEditor.C_PList();
}

}
