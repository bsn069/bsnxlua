using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace NBsn.NUpdateRes 
{


public class C_CheckVer : IDisposable
{
    public int          m_nTimeOutSec      = 5;
    public string       m_strVerFileName   = "check_ver.txt";
    public C_UpdateRes  m_pUpdateRes        = null;

    public static IEnumerator Run(C_UpdateRes pUpdateRes)
    {
        var pCheckVer = new C_CheckVer(pUpdateRes);
        return pCheckVer.Do();
    }

    private C_CheckVer(C_UpdateRes pUpdateRes)
    {
        m_pUpdateRes = pUpdateRes;
    }

    public IEnumerator Do()
    {
        var strUrl = m_pUpdateRes.GetBaseUrl() + m_strVerFileName;
        var pWeb = UnityWebRequest.Get(strUrl);
        pWeb.timeout = m_nTimeOutSec;
        yield return pWeb.Send();

        var pBinVer = new C_Version();
        pBinVer.m_majorVersion = 1;
        pBinVer.m_minorVersion = 2;
        m_pUpdateRes.m_pBinVer = pBinVer;

        var pResVer = new C_Version();
        pResVer.m_majorVersion = 1;
        pResVer.m_minorVersion = 2;
        m_pUpdateRes.m_pResVer = pResVer;
    }
}


}