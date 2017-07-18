using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using UnityEngine;

namespace NBsn.NEditor
{

public class C_PList
{
    public void LoadFile(string strPlistPath)
    {
		string fileContent = string.Empty;
        using (FileStream file = new FileStream(strPlistPath, FileMode.Open))
        {
            byte[] str = new byte[(int)file.Length];
            file.Read(str, 0, str.Length);
            fileContent = str.UTF8();
            // Debug.Log(fileContent);
            file.Close();
            file.Dispose();
        }
        //去掉<!DOCTYPE>,不然异常
        int delStart = fileContent.IndexOf("<!DOCTYPE");
        int delEnd = fileContent.IndexOf("\n", delStart);
        fileContent = fileContent.Remove(delStart, delEnd - delStart);
		LoadText(fileContent);
    }

    public void LoadText(string strXMLText)
    {
        // Debug.LogFormat("strXMLText={0}", strXMLText);
        XDocument xDoc = XDocument.Parse(strXMLText);
        LoadXDoc(xDoc);
    }

    public void LoadXDoc(XDocument xDoc)
    {
        var eDict = xDoc.Element("plist");
        var eDictElement = eDict.Element("dict");
        var eDictElements = eDictElement.Elements();

		var eDictMetadata = eDictElements.ElementAt(3);
		var eDictMetadatas = eDictMetadata.Elements();
		ParseMetadata(eDictMetadatas);

		var eDictFrame = eDictElements.ElementAt(1);
		var eDictFrames = eDictFrame.Elements();
		ParseFrames(eDictFrames);
    }

    private void ParseMetadata(IEnumerable<XElement> elements)
    {
		m_strTextureFileName = elements.ElementAt(3).Value; 
		Debug.LogFormat("m_strTextureFileName={0}", m_strTextureFileName);
    }

    private void ParseFrames(IEnumerable<XElement> elements)
    {
        for (int i = 0; i < elements.Count(); i += 2)
        {
            var strName = elements.ElementAt(i).Value;
            var eDict = elements.ElementAt(i + 1);
            var ePos = eDict.Elements().ElementAt(1);
            Debug.LogFormat("frame={0} {1}", strName, ePos.Value);
        }
    }

	private string m_strTextureFileName = null;
}

}