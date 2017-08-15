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
		if (m_eUIShowState == NBsn.E_UIShowState.NoInit) 
		{
			OnInit();
		}
		m_eUIShowState = NBsn.E_UIShowState.Showing;		
	}

	public virtual void OnShowEnd()
	{
		m_eUIShowState = NBsn.E_UIShowState.Show;
	}

	public virtual void OnHideBegin()
	{
		m_eUIShowState = NBsn.E_UIShowState.Hiding;
	}

	public virtual void OnHideEnd()
	{
		m_eUIShowState = NBsn.E_UIShowState.Hide;
	}

	public virtual void SetParent(ViewModel vm)
	{
		m_parent = vm;
	}

	public virtual ViewModel GetParent()
	{
		return m_parent;
	}

	public virtual void UnInit()
	{
		
	}

	protected virtual void OnInit()
	{
		
	}

	protected NBsn.E_UIShowState m_eUIShowState = NBsn.E_UIShowState.Hide;
	protected ViewModel m_parent = null;
}

}
