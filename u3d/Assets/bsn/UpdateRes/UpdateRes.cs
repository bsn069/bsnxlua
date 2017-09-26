using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace NBsn.NUpdateRes 
{

public class C_UpdateRes 
{
    public int               m_nTimeOutSec      = 5;
    public string            m_strBaseUrl       = "http://localhost:10001/win/";
    public UnityWebRequest   m_web              = null;
    public Coroutine         m_coroutine        = null;
    public C_Version         m_pBinVer          = null;
    public C_Version         m_pResVer          = null;
    public NBsn.NMVVM.UpdateVM m_pUpdateVM      = null;
    public string            m_strVerFileName   = "ver.txt";

    public C_UpdateRes()
    {
        m_pUpdateVM = NBsn.C_Global.Instance.UIMgr.GetVM("UIUpdate") as NBsn.NMVVM.UpdateVM;
    }

    public void GetVerInfo()
    {
        m_coroutine = NBsn.C_Global.Instance.Coroutine.Start(IGetVerInfo());
    }

    private IEnumerator IGetVerInfo()
    {
        m_pUpdateVM.TextCenter.Value = "获取版本信息";
        m_pUpdateVM.SliderValue.Value = 5;

        m_pUpdateVM.TextBinVer.Value = string.Format(
            "程序版本: {0}.{1}"
            , 0
            , 0
        );	

        m_pUpdateVM.TextResVer.Value = string.Format(
            "资源版本: {0}.{1}"
            , 0
            , 0
        );	

        var strUrl = GetBaseUrl() + m_strVerFileName;
        var pWeb = UnityWebRequest.Get(strUrl);
        pWeb.timeout = 5;
        pWeb.Send();

        while (!pWeb.isDone) {
            yield return Yielders.EndOfFrame;
            m_pUpdateVM.SliderValue.Value = (uint)(pWeb.downloadProgress * 95) + 5;
        }

        if (pWeb.isNetworkError) {
            m_pUpdateVM.TextCenter.Value = "网络错误 请检查网络";
            NBsn.C_Global.Instance.Log.Error(pWeb.error);
            pWeb.Dispose();
            yield break;
        }

        var verString = pWeb.downloadHandler.text;
        pWeb.Dispose();

        NBsn.C_Global.Instance.Log.Info(verString);

        m_pUpdateVM.TextCenter.Value = "获取版本信息完成";
        m_pUpdateVM.SliderValue.Value = 100;

        StringReader sr = new StringReader(verString);

        m_pBinVer = new C_Version();
        m_pBinVer.m_majorVersion = int.Parse(sr.ReadLine());
        m_pBinVer.m_minorVersion = int.Parse(sr.ReadLine());
        NBsn.C_Global.Instance.Log.InfoFormat(
            "BinVer {0}.{1}"
            , m_pBinVer.m_majorVersion
            , m_pBinVer.m_minorVersion
        );
		m_pUpdateVM.TextBinVer.Value = string.Format(
            "程序版本: {0}.{1}"
            , m_pBinVer.m_majorVersion
            , m_pBinVer.m_minorVersion
        );	

        m_pResVer = new C_Version();
        m_pResVer.m_majorVersion = int.Parse(sr.ReadLine());
        m_pResVer.m_minorVersion = int.Parse(sr.ReadLine());
        NBsn.C_Global.Instance.Log.InfoFormat(
            "ResVer {0}.{1}"
            , m_pResVer.m_majorVersion
            , m_pResVer.m_minorVersion
        );
        m_pUpdateVM.TextResVer.Value = string.Format(
            "资源版本: {0}.{1}"
            , m_pResVer.m_majorVersion
            , m_pResVer.m_minorVersion
        );	
    }

    public string GetBaseUrl()
    {
        return m_strBaseUrl;
    }

    private void UnInit()
    {
        if (m_coroutine != null) {
            NBsn.C_Global.Instance.Coroutine.Stop(m_coroutine);
            m_coroutine = null;
        }
    }
}

}
