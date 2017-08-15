using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NBsn.NMVVM
{

public class LoginV: View<LoginVM>
{
	public InputField m_inputName;
	public InputField m_inputPwd;
	public Button m_btnLogin;

	public LoginVM VM 
	{ 
		get { return (LoginVM)Context; } 
	}

	protected override void OnInit()
	{
		base.OnInit();
		m_btnLogin.onClick.AddListener(AddMember);
	}

	public void NameChanged()
	{
		VM.Name.Value = m_inputName.text;
	}

	public void PwdChanged()
	{
		VM.Pwd.Value = m_inputPwd.text;
	}

	public void OnClickLogin()
	{
		VM.Login();
	}
}

}
