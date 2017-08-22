using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBsn.NMVVM
{

public class MsgVM : ViewModel
{
	public readonly BindableProperty<string> Name = new BindableProperty<string>();
	public readonly BindableProperty<string> Desc = new BindableProperty<string>(); 

	#region init
	public override void OnInit()
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.NMVVM.MsgVM.OnInit()");

		Name.Value = "default name";	
		Desc.Value = "default desc";	
	}

	public override void OnUnInit()
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.NMVVM.MsgVM.OnUnInit()");
	}
	#endregion

	#region OnXXXChanged

	#endregion

	#region func

	#endregion
}

}
