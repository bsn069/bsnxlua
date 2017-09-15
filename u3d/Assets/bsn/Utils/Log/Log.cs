using UnityEngine;
using System.Collections;
using System;

namespace NBsn
{

public class C_Log {
	public void InfoFormat(string format, params object[] args) 
	{
		Debug.LogFormat(format, args);
	}

	public void ErrorFormat(string format, params object[] args) 
	{
		Debug.LogErrorFormat(format, args);
	}

	public void Info(object message) 
	{
		Debug.Log(message);
	}

	public void Error(object message) 
	{
		Debug.LogError(message);
	}

	#region init
	public bool Init() 
	{
		InfoFormat("NBsn.C_Log.Init()");
		m_LogFile = new C_LogFile();
		if (!m_LogFile.Init("log"))
		{
			return false;
		}
		return true;
	}

	public void UnInit() 
	{
		InfoFormat("NBsn.C_Log.UnInit()");
		if (m_LogFile != null)
		{
			m_LogFile.UnInit();
			m_LogFile = null;
		}
	}
	#endregion

	protected C_LogFile m_LogFile = null;
}

}