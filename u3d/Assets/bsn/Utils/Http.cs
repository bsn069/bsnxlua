using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace NBsn.NHttp 
{

public enum E_Method
{
    Get     = 0,
    Post    = 1,
}


public interface I_Request
{
    string GetUrl();
    E_Method GetMethod();
}


public interface I_Response
{
    string  GetData();
    string  GetError();
    long    GetStatusCode();
    bool    IsSuccess();
}


public class C_Request : I_Request
{
    public string GetUrl()
    {
        return m_strUrl;
    }

    public E_Method GetMethod()
    {
        return m_eMethod;
    }

    public E_Method m_eMethod   = E_Method.Get;
    public string   m_strUrl    = null;
}


public class C_Response : I_Response
{
    public string GetData()
    {
        return m_strData;
    }

    public string GetError()
    {
        return m_strError;
    }

    public long GetStatusCode()
    {
        return m_StatusCode;
    }

    public bool IsSuccess()
    {
        return m_bSuccess;
    }

    public string   m_strData       = null;
    public string   m_strError      = null;
    public long     m_StatusCode    = 0;
    public bool     m_bSuccess      = false;

}


public class C_Http
{
    public static IEnumerator Get(string strUrl, Action<I_Response> funcCallBack)
    {
        using (var www = UnityWebRequest.Get(strUrl))
        {
            yield return www.Send();
            ExecResponseCallBack(www, funcCallBack);
        }
    }

    //public static IEnumerator Post(string url, string parameters, Action<I_Response> funcCallBack)
    //{
    //    var formData = new List<IMultipartFormSection>
    //    {
    //        new MultipartFormDataSection(parameters)
    //    };

    //    using (var www = UnityWebRequest.Post(url, formData))
    //    {
    //        yield return www.Send();
    //        ExecResponseCallBack(www, funcCallBack);
    //    }
    //}

    private static void ExecResponseCallBack(UnityWebRequest www, Action<I_Response> funcCallBack)
    {
        if (funcCallBack == null) 
        {
            return;
        }

        var pResponse = GetResponse(www);
        funcCallBack(pResponse);
    }

    private static I_Response GetResponse(UnityWebRequest www)
    {
        var pResponse = new C_Response();
        pResponse.m_bSuccess     = !www.isNetworkError;
        pResponse.m_strError     = www.error;
        pResponse.m_StatusCode   = www.responseCode;
        pResponse.m_strData      = www.downloadHandler.text;
        return pResponse;
    }
}

     
}
