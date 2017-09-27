using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace NBsn.NUpdateRes 
{

public class C_UpdateRes 
{
    public C_UpdateRes()
    {
        m_pUpdateVM = NBsn.C_Global.Instance.UIMgr.GetVM("UIUpdate") as NBsn.NMVVM.UpdateVM;
    }

	public bool IsSuccess()
	{
		return m_bSuccess;
	}

    public IEnumerator Run()
    {
		m_pUpdateVM.TextCenter.Value = "版本更新";
        m_pUpdateVM.SliderValue.Value = 5;

        mc_strPathRoot.PathDirCreate();

		yield return NBsn.C_Global.Instance.Coroutine.Start(GetLocalVer());
		if (m_pLocalVer == null)
		{
			yield break;
		}

		yield return NBsn.C_Global.Instance.Coroutine.Start(GetServerVerFile());
		if (m_strServerVerFile == null)
		{
			yield break;
		}
        m_srServer = new StringReader(m_strServerVerFile);			

		yield return NBsn.C_Global.Instance.Coroutine.Start(GetServerVer());
		if (m_pServerVer == null)
		{
			yield break;
		}

		m_bSuccess = true;
	}

	private IEnumerator GetLocalVer()
	{
		m_pUpdateVM.TextCenter.Value = "获取本地版本信息";
        m_pUpdateVM.SliderValue.Value += 1;
        yield return Yielders.EndOfFrame;

		var strFilePath = mc_strPathRoot.PathCombine(m_strVerFileName);
		NBsn.C_Global.Instance.Log.InfoFormat("C_UpdateRes.GetLocalVer strFilePath={0}", strFilePath); 
        try 
        {
		    m_strLocalVerFile = File.ReadAllText(strFilePath, System.Text.Encoding.UTF8);
        } 
        catch (Exception e) 
        {
            m_strLocalVerFile = null;
        }

		if (string.IsNullOrEmpty(m_strLocalVerFile))
		{
			m_strLocalVerFile = @"0.0.0
0
";
			File.WriteAllText(strFilePath, m_strLocalVerFile, System.Text.Encoding.UTF8);
		}

		m_srLocal = new StringReader(m_strLocalVerFile);

		m_pLocalVer = new C_Version();
		var strLocalVer = m_srLocal.ReadLine();
		NBsn.C_Global.Instance.Log.InfoFormat("C_UpdateRes.GetLocalVer strLocalVer={0}", strLocalVer); 
		m_pLocalVer.FromString(strLocalVer);

		m_pUpdateVM.TextLocalVer.Value = string.Format(
            "客户端版本:{0}"
            , m_pLocalVer.ToString()
        );		
	}

    private IEnumerator GetServerVerFile()
	{
		m_pUpdateVM.TextCenter.Value = "获取服务器版本文件";
        m_pUpdateVM.SliderValue.Value += 1;
        yield return Yielders.EndOfFrame;

		var strVerUrl = string.Format(
			"{0}/{1}/{2}"
			, NBsn.C_Config.ServerResHttpUrl
			, NBsn.C_Platform.Name()
			, m_strVerFileName
		); 
		NBsn.C_Global.Instance.Log.InfoFormat("C_UpdateRes.GetServerVerFile strVerUrl={0}", strVerUrl); 

		var pWeb = UnityWebRequest.Get(strVerUrl);
        pWeb.timeout = 5;
        yield return pWeb.Send();

        if (pWeb.isNetworkError) {
            m_pUpdateVM.TextCenter.Value = "网络错误 请检查网络";
            NBsn.C_Global.Instance.Log.Error(pWeb.error);
            pWeb.Dispose();
            yield break;
        }
		m_strServerVerFile = pWeb.downloadHandler.text;
		pWeb.Dispose();
		pWeb = null;
	}

	private IEnumerator GetServerVer()
	{
		m_pUpdateVM.TextCenter.Value = "获取服务器版本信息";
        m_pUpdateVM.SliderValue.Value += 1;
        yield return Yielders.EndOfFrame;

		m_pServerVer = new C_Version(); 
		var strServerVer = m_srServer.ReadLine();
		NBsn.C_Global.Instance.Log.InfoFormat("C_UpdateRes.GetServerVer strServerVer={0}", strServerVer); 
		m_pServerVer.FromString(strServerVer);	

		m_pUpdateVM.TextServerVer.Value = string.Format(
            "服务器版本:{0}"
            , m_pServerVer.ToString()
        );	
	}

	private IEnumerator GetServerFile()
	{
		m_pUpdateVM.TextCenter.Value = "获取服务器版本信息";
        m_pUpdateVM.SliderValue.Value += 1;
        yield return Yielders.EndOfFrame;

		var strMustDownFileCount = m_srServer.ReadLine();
        UInt32 fileCount;
        strMustDownFileCount.ToUint32(0, out fileCount);
        List<string> strFilePaths = new List<string>();
        for (UInt32 i = 0; i < fileCount; i++) {
		    var strFileConfig = m_srServer.ReadLine();
            var strLine = strFileConfig.Split(' ');
            strFilePaths.Add(strLine[0]);            
        }
	}

	protected readonly string mc_strPathRoot = 
		Application.persistentDataPath.PathFormat()
		.PathCombine(NBsn.C_PathConfig.ServerResDirName)
		.Unique(false);
	public NBsn.NMVVM.UpdateVM m_pUpdateVM      = null;
    public string            m_strVerFileName   = "ver.txt";
	public bool 			 m_bSuccess 		= false;
	public string 			m_strServerVerFile = null;
	public string 			m_strLocalVerFile = null;
    public C_Version         m_pServerVer          = null;
    public C_Version         m_pLocalVer          = null;
	public StringReader 	m_srServer = null;
	public StringReader 	m_srLocal = null;
}

}
