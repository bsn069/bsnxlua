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
        m_pUpdateVM = NBsn.C_Global.Instance.UIMgr.GetVM("UIUpdate") 
            as NBsn.NMVVM.UpdateVM;
    }

	public bool IsSuccess()
	{
		return !m_bError;
	}

    public IEnumerator Run()
    {
		m_pUpdateVM.TextCenter.Value = "版本更新";
        m_pUpdateVM.SliderValue.Value = 5;

        mc_strPathRoot.PathDirCreate();

		yield return NBsn.C_Global.Instance.Coroutine.Start(GetLocalVer());

        var serverVerDownloadFileName = "server_" + NBsn.C_PathConfig.VerFileName;
		yield return NBsn.C_Global.Instance.Coroutine.Start(GetServerVerFile(serverVerDownloadFileName));
        if (m_bError) {
            yield break;
        }
		yield return NBsn.C_Global.Instance.Coroutine.Start(GetServerVer(serverVerDownloadFileName));
        yield return NBsn.C_Global.Instance.Coroutine.Start(DownloadServerLuaFile());
        yield return NBsn.C_Global.Instance.Coroutine.Start(DownloadABMainFile());
        yield return NBsn.C_Global.Instance.Coroutine.Start(DownloadABFile());
        yield return NBsn.C_Global.Instance.Coroutine.Start(SaveServerVerFile(serverVerDownloadFileName));
	}

    C_Version   m_pLocalVer = new C_Version();
    Dictionary<string, string> m_LocalLuaFile2Base64Md5 = new Dictionary<string, string>();
    Dictionary<string, string> m_LocalABFile2Base64Md5 = new Dictionary<string, string>();
	private IEnumerator GetLocalVer()
	{
		m_pUpdateVM.TextCenter.Value = "获取本地版本信息";
        m_pUpdateVM.SliderValue.Value += 1;
        yield return Yielders.EndOfFrame;

		var strFilePath = mc_strPathRoot.PathCombine(NBsn.C_PathConfig.VerFileName);
		NBsn.C_Global.Instance.Log.InfoFormat(
            "C_UpdateRes.GetLocalVer strFilePath={0}"
            , strFilePath
        );

        if (!File.Exists(strFilePath)) {
            yield break;
        } 

		var strVer = File.ReadAllText(
            strFilePath
            , new System.Text.UTF8Encoding(false)
        );
		var sr = new StringReader(strVer);
        yield return Yielders.EndOfFrame;

		var strLocalVer = sr.ReadLine();
		m_pLocalVer.FromString(strLocalVer);
		NBsn.C_Global.Instance.Log.InfoFormat(
            "C_UpdateRes.GetLocalVer strLocalVer={0}"
            , strLocalVer
        ); 
		m_pUpdateVM.TextLocalVer.Value = string.Format(
            "客户端版本:{0}"
            , strLocalVer
        );		
        yield return Yielders.EndOfFrame;

        ParseLuaVer(sr, ref m_LocalLuaFile2Base64Md5);
        ParseABVer(sr, ref m_LocalABFile2Base64Md5);
        yield return Yielders.EndOfFrame;
	}

    private IEnumerator GetServerVerFile(string serverVerDownloadFileName)
	{
		m_pUpdateVM.TextCenter.Value = "获取服务器版本文件";
        m_pUpdateVM.SliderValue.Value += 1;
        yield return Yielders.EndOfFrame;

        var strHttpFilePath = string.Format(
			"{0}/{1}"
			, NBsn.C_Platform.Name()
			, C_PathConfig.VerFileName
		); 
        var strLocalFilePath = mc_strPathRoot
            .PathCombine(serverVerDownloadFileName)
            ;
        yield return NBsn.C_Global.Instance.Coroutine.Start(
            DownloadServerFile(strHttpFilePath, strLocalFilePath)
        );
    }

    private C_Version   m_pServerVer    = new C_Version();
    private Dictionary<string, string> m_ServerLuaFile2Base64Md5 = new Dictionary<string, string>();
    private Dictionary<string, string> m_ServerABFile2Base64Md5 = new Dictionary<string, string>();
    private IEnumerator GetServerVer(string serverVerDownloadFileName)
	{
		m_pUpdateVM.TextCenter.Value = "获取服务器版本";
        m_pUpdateVM.SliderValue.Value += 1;
        yield return Yielders.EndOfFrame;

        var strFilePath = mc_strPathRoot.PathCombine(serverVerDownloadFileName);
		NBsn.C_Global.Instance.Log.InfoFormat(
            "C_UpdateRes.GetServerVer strFilePath={0}"
            , strFilePath
        );
		var strVer = File.ReadAllText(
            strFilePath
            , new System.Text.UTF8Encoding(false)
        );
		var sr = new StringReader(strVer);
        yield return Yielders.EndOfFrame;

        var strServerVer = sr.ReadLine();
		m_pServerVer.FromString(strServerVer);
		NBsn.C_Global.Instance.Log.InfoFormat(
            "C_UpdateRes.GetServerVer strServerVer={0}"
            , strServerVer
        ); 
		m_pUpdateVM.TextServerVer.Value = string.Format(
            "服务端版本:{0}"
            , strServerVer
        );		
        yield return Yielders.EndOfFrame;

        ParseLuaVer(sr, ref m_ServerLuaFile2Base64Md5);
        ParseABVer(sr, ref m_ServerABFile2Base64Md5);
        yield return Yielders.EndOfFrame;
	}

    private IEnumerator DownloadServerLuaFile()
	{
		m_pUpdateVM.TextCenter.Value = "下载服务器lua文件信息";
        yield return Yielders.EndOfFrame;

        string strBase64Md5;
        foreach (var item in m_ServerLuaFile2Base64Md5) 
        {
            if (m_LocalLuaFile2Base64Md5.TryGetValue(item.Key, out strBase64Md5)) 
            {
                if (strBase64Md5 == item.Value) 
                {
                    continue;
                }
            }

            var strHttpFilePath = NBsn.C_PathConfig.LuaDirName + "/" + item.Key;
            var strLocalFilePath = mc_strPathRoot
                .PathCombine(NBsn.C_PathConfig.LuaDirName)
                .PathCombine(item.Key)
                .PathFormat()
                ;
            yield return NBsn.C_Global.Instance.Coroutine.Start(
                DownloadServerFile(strHttpFilePath, strLocalFilePath)
            );
            if (m_bError) {
                break;
            }
        }
	}

    private IEnumerator DownloadABMainFile()
	{
		m_pUpdateVM.TextCenter.Value = "下载服务器AB文件信息";
        yield return Yielders.EndOfFrame;

        var strHttpFilePath = string.Format(
			"{0}/{0}"
			, NBsn.C_Platform.Name()
		); 
        var strLocalFilePath = mc_strPathRoot
            .PathCombine(NBsn.C_Platform.Name())
            .PathFormat()
            ;
        yield return NBsn.C_Global.Instance.Coroutine.Start(
            DownloadServerFile(strHttpFilePath, strLocalFilePath)
        );
	}

	private IEnumerator DownloadABFile()
	{
		m_pUpdateVM.TextCenter.Value = "下载服务器.ab文件";
        yield return Yielders.EndOfFrame;

        string strBase64Md5;
        foreach (var item in m_ServerABFile2Base64Md5) 
        {
            if (m_LocalABFile2Base64Md5.TryGetValue(item.Key, out strBase64Md5)) 
            {
                if (strBase64Md5 == item.Value) 
                {
                    continue;
                }
            }

            var strHttpFilePath = NBsn.C_Platform.Name() + "/" + item.Key;
            var strLocalFilePath = mc_strPathRoot
				.PathCombine(NBsn.C_PathConfig.APPABResDir)
                .PathCombine(item.Key)
                .PathFormat()
                ;
            yield return NBsn.C_Global.Instance.Coroutine.Start(
                DownloadServerFile(strHttpFilePath, strLocalFilePath)
            );
            if (m_bError) {
                break;
            }
        }
	}

    private IEnumerator SaveServerVerFile(string serverVerDownloadFileName)
	{
		m_pUpdateVM.TextCenter.Value = "保存服务器版本文件";
        yield return Yielders.EndOfFrame;

        var strLocalFilePath = mc_strPathRoot.PathCombine(NBsn.C_PathConfig.VerFileName);
        var strServerFilePath = mc_strPathRoot.PathCombine(serverVerDownloadFileName);

        File.Delete(strLocalFilePath);
        File.Move(strServerFilePath, strLocalFilePath);
    }

    private IEnumerator DownloadServerFile(
        string strHttpFilePath
        , string strLocalFilePath
    )
	{
		NBsn.C_Global.Instance.Log.InfoFormat(
            "C_UpdateRes.DownloadServerFile strHttpFilePath={0}, strLocalFilePath={1}"
            , strHttpFilePath
            , strLocalFilePath
        ); 

		m_pUpdateVM.TextCenter.Value = "下载服务端文件:" + strHttpFilePath;
        yield return Yielders.EndOfFrame;

        var strUrl = string.Format(
			"{0}{1}"
			, NBsn.C_Config.ServerResHttpUrl
			, strHttpFilePath
		); 
		NBsn.C_Global.Instance.Log.InfoFormat(
            "C_UpdateRes.DownloadServerFile strUrl={0}"
            , strUrl
        ); 

		var pWeb = UnityWebRequest.Get(strUrl);
        pWeb.timeout = 5;
        yield return pWeb.Send();

        if (pWeb.isNetworkError || pWeb.responseCode != 200) 
        {
            m_bError = true;
            if (pWeb.isNetworkError) 
            {
                m_pUpdateVM.TextCenter.Value = "网络错误 请检查网络";
            }
            else
            {
                if (pWeb.responseCode == 404) 
                {
                    m_pUpdateVM.TextCenter.Value = "服务端没有此文件";
                }
                else
                {
                    m_pUpdateVM.TextCenter.Value = pWeb.error;
                }
            }
            NBsn.C_Global.Instance.Log.Error(pWeb.error);
            pWeb.Dispose();
            yield break;
        }
        NBsn.CommonEx.ReWriteFile(strLocalFilePath, pWeb.downloadHandler.data);
		pWeb.Dispose();
		pWeb = null;
	}

    public static void ParseLuaVer(
        StringReader sr
        , ref Dictionary<string, string> luaFile2Base64Md5
    )
	{
		NBsn.C_Global.Instance.Log.Info("C_UpdateRes.ParseLuaFile"); 

        var strTemp = sr.ReadLine();
        UInt32 u32Count;
        strTemp.ToUint32(0, out u32Count);

        for (UInt32 i = 0; i < u32Count; i++) {
            strTemp = sr.ReadLine();
            var strArr = strTemp.Split(',');
            luaFile2Base64Md5.Add(strArr[0], strArr[1]);
        }
	}

    public static void ParseABVer(
        StringReader sr
        , ref Dictionary<string, string> abFile2Base64Md5
    )
	{
		NBsn.C_Global.Instance.Log.Info("C_UpdateRes.ParseABVer"); 

        var strTemp = sr.ReadLine();
        UInt32 u32Count;
        strTemp.ToUint32(0, out u32Count);

        for (UInt32 i = 0; i < u32Count; i++) {
            strTemp = sr.ReadLine();
            var strArr = strTemp.Split(',');
            abFile2Base64Md5.Add(strArr[0], strArr[1]);
        }
	}

	private readonly string mc_strPathRoot = 
		Application.persistentDataPath.PathFormat()
		.PathCombine(NBsn.C_PathConfig.ServerResDirName)
		.Unique(false);
	private NBsn.NMVVM.UpdateVM m_pUpdateVM  = null;
	private bool m_bError    = false;
}

}
