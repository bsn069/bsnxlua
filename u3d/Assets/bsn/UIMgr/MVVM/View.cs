using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NBsn.NMVVM
{


public interface I_View
{
	void Show(Action actionOnShowAfter);
	void Hide(Action actionOnHideAfter);	
	ViewModel GetVM();
}


public abstract class View<T> : MonoBehaviour, I_View where T:ViewModel
{
	#region mono behaviour
	protected virtual void Awake()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.View<{0}>.Awake()", typeof(T));

		OnInit();
	}

	protected virtual void OnDestroy()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.View<{0}>.OnDestroy()", typeof(T));
		OnUnInit();
	}
	#endregion

	#region init
	protected virtual void OnInit()
	{
		
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.View<{0}>.OnInit()", typeof(T));
	}
	
	protected virtual void OnUnInit()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.View<{0}>.OnUnInit()", typeof(T));
		SetVM(null);		
	}
	#endregion

	#region vm
	protected virtual void SetVM(T vm)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.View<{0}>.SetVM()", typeof(T));
		if (m_vm == vm)
		{
			return;
		}

		if (m_vm != null)
		{			
			Binder.Unbind(m_vm);
			m_vm.OnUnInit();
		}
		m_vm = vm;
		if (m_vm != null)
		{			
			Binder.Bind(m_vm);
			m_vm.OnInit();
		}
	}

	public virtual ViewModel GetVM()
	{
		return m_vm;
	}
	#endregion

	#region show
	public virtual void Show(Action actionOnShowAfter)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.View<{0}>.Show()", typeof(T));
		m_actionOnShowAfter = actionOnShowAfter;
		OnShowBegin();
		OnShow();
		OnShowEnd();
	}

	protected virtual void OnShowBegin()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.View<{0}>.OnShowBegin()", typeof(T));
		gameObject.SetActive(true);
		GetVM().OnShowBegin();
	}

	protected virtual void OnShow()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.View<{0}>.OnShow()", typeof(T));
		
	}

	protected virtual void OnShowEnd()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.View<{0}>.OnShowEnd()", typeof(T));
		GetVM().OnShowEnd();
		if (m_actionOnShowAfter != null)
		{
			m_actionOnShowAfter();
		}
	}
	#endregion

	#region hide
	public virtual void Hide(Action actionOnHideAfter)
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.View<{0}>.Hide()", typeof(T));
		m_actionOnHideAfter = actionOnHideAfter;
		OnHideBegin();
		OnHide();
		OnHideEnd();
	}

	protected virtual void OnHideBegin()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.View<{0}>.OnHideBegin()", typeof(T));
		GetVM().OnHideBegin();
	}

	protected virtual void OnHide()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.View<{0}>.OnHide()", typeof(T));
	}

	protected virtual void OnHideEnd()
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.NMVVM.View<{0}>.OnHideEnd()", typeof(T));
		gameObject.SetActive(false);
		GetVM().OnHideEnd();
		if (m_actionOnHideAfter != null)
		{
			m_actionOnHideAfter();
		}
	}
	#endregion

	protected T 		m_vm 				= null;
	protected Action 	m_actionOnShowAfter = null;
	protected Action 	m_actionOnHideAfter = null;
	protected readonly PropertyBinder<T> Binder=new PropertyBinder<T>();
}

}
