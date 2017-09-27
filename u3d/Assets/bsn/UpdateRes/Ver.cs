using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace NBsn.NUpdateRes 
{

public class C_Ver {
    C_Version   m_pVer  = new C_Version();

    public void SetVer(UInt16 binVer, UInt32 date, UInt16 dayIndex)
    {
        m_pVer.m_binVer     = binVer;
        m_pVer.m_date       = date;
        m_pVer.m_dayIndex   = dayIndex;
    }

    public void LoadFromString(string strData)
    {
        var sr = new StringReader(strData);			

        var strVer = sr.ReadLine();
		NBsn.C_Global.Instance.Log.InfoFormat("C_UpdateRes.GetServerVer strServerVer={0}", strServerVer); 
		m_pVer.FromString(strVer);	
    }

    public string ToString()
    {
        StringBuilder sb = new StringBuilder();
        return null;
    }
}

}