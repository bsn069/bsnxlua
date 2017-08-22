using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBsn.NMVVM
{

public class LoginVM : ViewModel
{
	public readonly BindableProperty<string> Name = new BindableProperty<string>();
	public readonly BindableProperty<string> Pwd = new BindableProperty<string>(); 

	public readonly BindableProperty<string> NameDefault = new BindableProperty<string>();

	#region init
	public override void OnInit()
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.NMVVM.LoginVM.OnInit()");

		NameDefault.Value = "default name";	
		Name.Value = "default name";	

		Name.OnValueChanged += OnNameChanged;
		Pwd.OnValueChanged 	+= OnPwdChanged;	
	}

	public override void OnUnInit()
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.NMVVM.LoginVM.OnUnInit()");

		Name.OnValueChanged -= OnNameChanged;
		Pwd.OnValueChanged 	-= OnPwdChanged;	
	}
	#endregion

	#region OnXXXChanged
	private void OnNameChanged(string oldValue, string newValue)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.LoginVM.OnNameChanged() oldValue={0}, newValue={1}", oldValue, newValue);
	}

	private void OnPwdChanged(string oldValue, string newValue)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.LoginVM.OnPwdChanged() oldValue={0}, newValue={1}", oldValue, newValue);
	}
	#endregion

	#region func
	public void Login()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.LoginVM.Login() Name={0}, Pwd={1}", Name, Pwd);

		var pView = NBsn.C_Global.Instance.UIMgr.GetView("UIMsg");
		pView.Show(null);
		var pVM = pView.GetVM() as MsgVM;
		pVM.Name.Value = "aaaaaaaaa";
		pVM.Desc.Value = "aaaaaaaaa";
	}
	#endregion
}

}
