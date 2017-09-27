
using System;
namespace NBsn.NUpdateRes 
{


public class C_Version 
{
    public UInt32 m_date 		= 0;
    public UInt16 m_dayIndex 	= 0;
    public UInt16 m_binVer 	= 0;

	public void FromString(string strVer)
	{
		var strArr = strVer.Split('.');
		
		strArr[0].ToUint16(0,out m_binVer);
		strArr[1].ToUint32(0,out m_date);
		strArr[2].ToUint16(0,out m_dayIndex);
	}

	public string ToString()
	{
		return string.Format("{0}.{1}.{2}", m_binVer, m_date, m_dayIndex);
	}
}


}