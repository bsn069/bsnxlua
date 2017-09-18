using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace NBsn.NHttp 
{

public class C_Http
{
    public static IEnumerator Get(
        string strUrl
        , Action<UnityWebRequest> funcCallBack
        , int nTimeOutSec
    )
    {
        using (var www = UnityWebRequest.Get(strUrl))
        {
            www.timeout = nTimeOutSec;
            yield return www.Send();
            if (funcCallBack != null) 
            {
                funcCallBack(www);
            }
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
}

     
}
