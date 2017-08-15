using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NBsn.NMVVM
{

public abstract class View<T> where T:ViewModel
{
	public T Context
	{
		get { return ViewModelProperty.Value; }
		set
		{
			if (!m_bInit)
			{
				OnInit();
				m_bInit = true;
			}
			ViewModelProperty.Value = value;
		}
	}

	// 绑定的上下文发生改变时
	public virtual void OnContextChanged(T oldValue, T newValue)
	{
		Binder.Unbind(oldValue);
		Binder.Bind(newValue);
	}

	#region show
	public void Show(Action actionOnShowAfter)
	{
		m_actionOnShowAfter = actionOnShowAfter;
		OnShowBegin();
		OnShow();
		OnShowEnd();
	}

	public virtual void OnShowBegin()
	{
		gameObject.SetActive(true);
		Context.OnShowBegin();
	}

	public virtual void OnShow()
	{
		
	}

	public virtual void OnShowEnd()
	{
		Context.OnShowEnd();
		if (m_actionOnShowAfter != null)
		{
			m_actionOnShowAfter();
		}
	}
	#endregion

	#region hide
	public void Hide(Action actionOnHideAfter)
	{
		m_actionOnHideAfter = actionOnHideAfter;
		OnHideBegin();
		OnHide();
		OnHideEnd();
	}

	public virtual void OnHideBegin()
	{
		Context.OnHideBegin();
	}

	public virtual void OnHide()
	{
		
	}

	public virtual void OnHideEnd()
	{
		gameObject.SetActive(false);
		Context.OnHideEnd();
		if (m_actionOnHideAfter != null)
		{
			m_actionOnHideAfter();
		}
	}
	#endregion

	protected virtual void OnInit()
	{
		ViewModelProperty.OnValueChanged += OnContextChanged;
	}

	protected readonly PropertyBinder<T> Binder=new PropertyBinder<T>();
	private readonly BindableProperty<T> ViewModelProperty = new BindableProperty<T>();
	private bool m_bInit = false;
	protected Action m_actionOnShowAfter = null;
	protected Action m_actionOnHideAfter = null;
}

}
