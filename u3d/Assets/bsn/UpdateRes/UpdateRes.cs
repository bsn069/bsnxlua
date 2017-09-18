using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace NBsn.NUpdateRes 
{

public class C_Version 
{
    public int m_majorVersion = 0;
    public int m_minorVersion = 0;
}

public class C_UpdateRes 
{
    public int               m_nTimeOutSec      = 5;
    public string            m_strBaseUrl       = "http://localhost:10001/Win/";
    public UnityWebRequest   m_web              = null;
    public Coroutine         m_coroutine        = null;
    public C_Version         m_pBinVer          = null;
    public C_Version         m_pResVer          = null;

    public static bool Run()
    {
        var pUpdateRes = new C_UpdateRes();
        pUpdateRes.Do();
        pUpdateRes.UnInit();
        pUpdateRes = null;
        return true;
    }

    public string GetBaseUrl()
    {
        return m_strBaseUrl;
    }

    private void Do()
    {
        m_coroutine = NBsn.C_Global.Instance.Coroutine.Start(Exec());
    }

    private void UnInit()
    {
        if (m_coroutine != null) {
            NBsn.C_Global.Instance.Coroutine.Stop(m_coroutine);
            m_coroutine = null;
        }
    }

    private IEnumerator Exec()
    {
        yield return C_CheckVer.Run(this);
    }
}

}
