using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace NBsn.NMVVM
{

public class UpdateV: View<UpdateVM>
{
	#region UI
	public Slider 	m_slider;
	public Text 	m_textCenter;
	#endregion

	public UpdateVM VM 
	{ 
		get { return (UpdateVM)GetVM(); } 
	}

	#region init
	protected override void OnInit()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.UpdateV.OnInit()");

		Binder.Add<string>("TextCenter", VMOnTextCenterChanged);
		Binder.Add<uint>("SliderValue", VMOnSliderValueChanged);

		var vm = new UpdateVM();
		SetVM(vm);
	}
	#endregion

	#region UIXXX
	public void UIOnClickHide()
	{
		NBsn.C_Global.Instance.Log.Info("NBsn.NMVVM.LoginV.UIOnClickHide()");

		Hide();
	}
	#endregion

	#region VMXXX
	private void VMOnTextCenterChanged(string oldValue, string newValue)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.UpdateV.VMOnTextCenterChanged() oldValue={0} newValue={1}", oldValue, newValue);

		m_textCenter.text = newValue;
	}

	private void VMOnSliderValueChanged(uint oldValue, uint newValue)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.UpdateV.VMOnSliderValueChanged() oldValue={0} newValue={1}", oldValue, newValue);

		m_slider.value = newValue;
	}
	#endregion
}

}
