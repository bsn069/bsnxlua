using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace NBsn.NMVVM
{

public class MsgV: View<MsgVM>
{
	#region UI
	public Text 	m_textName;
	public Text 	m_textDesc;
	#endregion

	public MsgVM VM 
	{ 
		get { return (MsgVM)GetVM(); } 
	}

	#region init
	protected override void OnInit()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.MsgV.OnInit()");

		Binder.Add<string>("Name", VMOnNameChanged);
		Binder.Add<string>("Desc", VMOnDescChanged);

		var vm = new MsgVM();
		SetVM(vm);
	}
	#endregion

	#region UIXXX
	
	#endregion

	#region VMXXX
	private void VMOnNameChanged(string oldValue, string newValue)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.MsgV.VMOnNameChanged() oldValue={0} newValue={1}", oldValue, newValue);

		m_textName.text = newValue;
	}

	private void VMOnDescChanged(string oldValue, string newValue)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.MsgV.VMOnDescChanged() oldValue={0} newValue={1}", oldValue, newValue);

		m_textDesc.text = newValue;
	}
	#endregion
}

}
