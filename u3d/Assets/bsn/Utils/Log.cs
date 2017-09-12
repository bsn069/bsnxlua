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
	public void Init() 
	{
		InfoFormat("NBsn.C_Log.Init()");
	}

	public void UnInit() 
	{
		InfoFormat("NBsn.C_Log.UnInit()");
	}
	#endregion
}

}