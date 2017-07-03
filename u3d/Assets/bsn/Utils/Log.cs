using UnityEngine;
using System.Collections;
using System;

namespace NBsn {

public class CLog {
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
	public void Init() 
	{
		InfoFormat("NBsn.CLog.Init()");
	}

	public void UnInit() 
	{
		InfoFormat("NBsn.CLog.UnInit()");
	}
	#endregion
}

}