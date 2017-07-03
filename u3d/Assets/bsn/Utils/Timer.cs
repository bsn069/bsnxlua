using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Diagnostics;

namespace NBsn 
{

/*
using(NBsn.CTimer timer = new NBsn.CTimer("FindReferences")) {
	FindReferencesImp();
}
*/
public class CTimer : IDisposable  
{
	private string      m_strName   = null;
	private Stopwatch   m_stopWatch = null;
	
	public CTimer(string strName) 
	{
		m_strName = strName;
		m_stopWatch = Stopwatch.StartNew();
	}

	public void Dispose() 
	{
		m_stopWatch.Stop();
		UnityEngine.Debug.LogFormat(
			"Timer[{0}] [{1}]MS"
			, m_strName
			, m_stopWatch.ElapsedMilliseconds
		);
	}
}  

}