using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBsn.NMVVM
{

public class UpdateVM : ViewModel
{
	public readonly BindableProperty<string> TextCenter = new BindableProperty<string>();
	public readonly BindableProperty<string> TextName = new BindableProperty<string>();
	public readonly BindableProperty<string> TextDesc = new BindableProperty<string>();
	public readonly BindableProperty<string> TextLocalVer = new BindableProperty<string>();
	public readonly BindableProperty<string> TextServerVer = new BindableProperty<string>();
	public readonly BindableProperty<uint> SliderValue = new BindableProperty<uint>(); 

	#region init
	public override void OnInit()
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.NMVVM.UpdateVM.OnInit()");

		TextCenter.Value = "0%";	
		TextName.Value = "";	
		TextDesc.Value = "";	
        var strServerVer = "服务端版本:0.0.0";	
		NBsn.C_Global.Instance.Log.InfoFormat("strServerVer={0}", strServerVer);
        TextServerVer.Value = strServerVer;

        var strLocalVer = "客户端版本:0.0.0";	
        UnityEngine.Debug.Log(strLocalVer);
		NBsn.C_Global.Instance.Log.InfoFormat("strLocalVer={0}", strLocalVer);
        TextLocalVer.Value = strLocalVer;	

		SliderValue.Value = 1;	
	}

	public override void OnUnInit()
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.NMVVM.UpdateVM.OnUnInit()");
	}
	#endregion

	#region OnXXXChanged

	#endregion

	#region func

	#endregion
}

}
