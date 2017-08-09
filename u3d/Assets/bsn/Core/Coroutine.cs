using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

namespace NBsn 
{

public class C_Coroutine 
{
	public Coroutine Start(IEnumerator iEnumerator) 
	{
#if UNITY_EDITOR
		if (m_Mono == null) {
			NBsn.C_Global.Instance.Log.InfoFormat("m_Mono == null"); 
			return null;
		}
#endif
		return m_Mono.StartCoroutine(iEnumerator);
	}

	public void Stop(Coroutine coroutine) 
	{
#if UNITY_EDITOR
		if (m_Mono == null) {
			NBsn.C_Global.Instance.Log.InfoFormat("m_Mono == null"); 
			return;
		}
#endif
		m_Mono.StopCoroutine(coroutine);
	}

	#region init
	public void Init(MonoBehaviour Mono) 
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_Coroutine.Init()");
		m_Mono = Mono;
	}

	public void UnInit() 
	{
		NBsn.C_Global.Instance.Log.InfoFormat("NBsn.C_Coroutine.UnInit()");
		m_Mono = null;
	}
	#endregion

	private MonoBehaviour m_Mono = null;
}

}