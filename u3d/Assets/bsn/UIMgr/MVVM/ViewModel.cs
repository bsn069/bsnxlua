using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBsn.NMVVM
{

public class ViewModel
{
	public virtual void OnShowBegin()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.ViewModel.OnShowBegin() m_eUIShowState={0}", m_eUIShowState);

		m_eUIShowState = NBsn.E_UIShowState.Showing;		
	}

	public virtual void OnShowEnd()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.ViewModel.OnShowEnd() m_eUIShowState={0}", m_eUIShowState);

		m_eUIShowState = NBsn.E_UIShowState.Show;
	}

	public virtual void OnHideBegin()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.ViewModel.OnHideBegin() m_eUIShowState={0}", m_eUIShowState);

		m_eUIShowState = NBsn.E_UIShowState.Hiding;
	}

	public virtual void OnHideEnd()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.ViewModel.OnHideEnd() m_eUIShowState={0}", m_eUIShowState);

		m_eUIShowState = NBsn.E_UIShowState.Hide;
	}

	public virtual void SetParent(ViewModel vm)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.ViewModel.SetParent() m_eUIShowState={0}", m_eUIShowState);

		m_parent = vm;
	}

	public virtual ViewModel GetParent()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.ViewModel.GetParent() m_eUIShowState={0}", m_eUIShowState);

		return m_parent;
	}

	#region init
	public virtual void OnInit()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.ViewModel.OnInit() m_eUIShowState={0}", m_eUIShowState);		
	}
	
	public virtual void OnUnInit()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.ViewModel.OnUnInit() m_eUIShowState={0}", m_eUIShowState);		
	}
	#endregion

	protected NBsn.E_UIShowState m_eUIShowState = NBsn.E_UIShowState.Hide;
	protected ViewModel m_parent = null;
}

}
