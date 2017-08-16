using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace NBsn.NMVVM
{

public class LoginV: View<LoginVM>
{
	#region UI
	public InputField 	m_inputName;
	public InputField 	m_inputPwd;
	public Button 		m_btnLogin;
	#endregion

	public LoginVM VM 
	{ 
		get { return (LoginVM)GetVM(); } 
	}

	#region init
	protected override void OnInit()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.LoginV.OnInit()");

		Binder.Add<string>("NameDefault", VMOnNameDefaultChanged);

		var vm = new LoginVM();
		SetVM(vm);
	}
	#endregion

	#region UIXXX
	public void UIOnNameChanged()
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.NMVVM.LoginV.UIOnNameChanged()");

		VM.Name.Value = m_inputName.text;
	}

	public void UIOnPwdChanged()
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.NMVVM.LoginV.UIOnPwdChanged()");

		VM.Pwd.Value = m_inputPwd.text;
	}

	public void UIOnClickLogin()
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.NMVVM.LoginV.UIOnClickLogin()");

		VM.Login();
	}
	#endregion

	#region VMXXX
	private void VMOnNameDefaultChanged(string oldValue, string newValue)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.LoginV.VMOnNameDefaultChanged() oldValue={0} newValue={1}", oldValue, newValue);

		m_inputName.text = newValue;
	}
	#endregion
}

}
