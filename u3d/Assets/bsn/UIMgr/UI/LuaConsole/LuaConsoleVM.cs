using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NBsn.NMVVM
{

public class LuaConsoleVM : ViewModel
{
	public readonly BindableProperty<string> TextOutput = new BindableProperty<string>();

	#region init
	public override void OnInit()
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.NMVVM.LuaConsoleVM.OnInit()");

		TextOutput.Value = "";	
	}

	public override void OnUnInit()
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.NMVVM.LuaConsoleVM.OnUnInit()");
	}
	#endregion

	#region OnXXXChanged

	#endregion

	#region func
	public void RunLua(string strLua)
	{
		TextOutput.Value = "";	
		Application.logMessageReceived += LogCallback;
		NBsn.C_Global.Instance.Lua.DoString(strLua: strLua);
		Application.logMessageReceived -= LogCallback;
	}

	private void LogCallback(string condition, string stackTrace, LogType type) 
	{
		TextOutput.Value += condition + "\r\n";	
	}
	#endregion
}

}
