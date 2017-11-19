using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace NBsn.NMVVM
{

public class LuaConsoleV: View<LuaConsoleVM>
{
	#region UI
	public InputField 	m_inputLua;
	public Text 	m_textOutput;
	public Button 		m_btnRun;
	public Button 		m_btnClear;
	public Button 		m_btnClose;
	#endregion

	public LuaConsoleVM VM 
	{ 
		get { return (LuaConsoleVM)GetVM(); } 
	}

	#region init
	protected override void OnInit()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.LuaConsoleV.OnInit()");

		Binder.Add<string>("TextOutput", VMOnTextOutputChanged);

		var vm = new LuaConsoleVM();
		SetVM(vm);
	}
	#endregion

	#region UIXXX
	public void UIOnClickRun()
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.NMVVM.LoginV.UIOnClickRun()");
		VM.RunLua(m_inputLua.text);
	}

	public void UIOnClickClear()
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.NMVVM.LoginV.UIOnClickClear()");
		m_inputLua.text = "";
	}
	#endregion	

	#region VMXXX
	private void VMOnTextOutputChanged(string oldValue, string newValue)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.LuaConsoleV.VMOnTextOutputChanged() oldValue={0} newValue={1}", oldValue, newValue);

		m_textOutput.text = newValue;
	}
	#endregion
}

}
