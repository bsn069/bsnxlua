
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;
using XLua;

namespace NBsn.NEditor 
{

public static class C_Test
{
    [MenuItem("Bsn/T")]
	private static void t1()
	{
		NBsn.C_Global.EditorBegin();
		LuaTable table = NBsn.C_Global.Instance.Lua.NewTable();
		NBsn.C_Global.Instance.Lua.DoString("require 'editor.ab_config'");
		NBsn.C_Global.Instance.Lua.Get("g_tTemp", out table);
		NBsn.C_Global.Instance.Log.Info(table);
		table.ForEach<string, LuaTable>((strPath, tb)=>{
			NBsn.C_Global.Instance.Log.Info(strPath);
			string strAB;
			tb.Get("strAssetBundleOutPath", out strAB);
			NBsn.C_Global.Instance.Log.Info(strAB);

			var strAssetsPath = strPath.PathFormat();
			Debug.LogFormat("SetPrefabABName strAssetsPath={0}", strAssetsPath); 
            var importer = AssetImporter.GetAtPath(strAssetsPath);
            if (importer == null) {
                Debug.LogErrorFormat("basePath={0} importer == null", strAssetsPath);
                return;
            }

			var strABPath = strAB;
            importer.SetAssetBundleNameAndVariant(strABPath, null);
		});

		NBsn.C_Global.EditorEnd();
    }
}

}
