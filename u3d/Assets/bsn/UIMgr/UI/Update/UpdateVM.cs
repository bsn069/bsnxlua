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
	public readonly BindableProperty<string> TextBinVer = new BindableProperty<string>();
	public readonly BindableProperty<string> TextResVer = new BindableProperty<string>();
	public readonly BindableProperty<uint> SliderValue = new BindableProperty<uint>(); 

	#region init
	public override void OnInit()
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.NMVVM.UpdateVM.OnInit()");

		TextCenter.Value = "0%";	
		TextName.Value = "";	
		TextDesc.Value = "";	
        TextBinVer.Value = string.Format(
            "程序版本: {0}.{1}"
            , 0
            , 0
        );	

        TextResVer.Value = string.Format(
            "资源版本: {0}.{1}"
            , 0
            , 0
        );	

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
