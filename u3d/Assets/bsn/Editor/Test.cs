
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
		

		NBsn.C_Global.EditorEnd();
    }
}

}
