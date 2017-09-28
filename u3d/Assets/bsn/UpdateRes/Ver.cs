using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace NBsn.NUpdateRes 
{

public class C_Ver {
    public C_Version   m_pVer  = new C_Version();
    public Dictionary<string, string> m_lua = new Dictionary<string, string>();

    public void SetVer(UInt16 binVer, UInt32 date, UInt16 dayIndex)
    {
        m_pVer.m_binVer     = binVer;
        m_pVer.m_date       = date;
        m_pVer.m_dayIndex   = dayIndex;
    }

    public void FromString(string strData)
    {
        var sr = new StringReader(strData);			

        var strVer = sr.ReadLine();
		NBsn.C_Global.Instance.Log.InfoFormat("C_Ver.FromString strVer={0}", strVer); 
		m_pVer.FromString(strVer);	

        var strTemp = sr.ReadLine();
        UInt32 u32Count;
        strTemp.ToUint32(0, out u32Count);
        for (UInt32 i = 0; i < u32Count; i++) {
            strTemp = sr.ReadLine();
            var strArr = strTemp.Split(',');
            m_lua.Add(strArr[0], strArr[1]);
        }
    }
}

}