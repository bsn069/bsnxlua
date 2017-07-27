using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace NBsn.NEditor
{

public class C_PList
{
    public bool SetPlistPath(string strPlistPath)
	{
		m_strPlistPath = strPlistPath;
		m_bHadLoad = false;
		return true;
	}	

	public bool LoadFromXmlString(string strXmlString)
    {
		try 
		{
			XDocument xDoc = XDocument.Parse(strXmlString);
			LoadXDoc(xDoc);
			m_bHadLoad = true;			
			return true;
		}
		catch (Exception e) 
		{
            Debug.LogError(e.ToString());
            return false;
        }
    }

	public bool LoadFromFile()
    {
		if (m_strPlistPath == null)
		{
			Debug.LogError("m_strPlistPath = null");
			return false;
		}

		try 
		{
			string strXmlContent = string.Empty;
			using (FileStream file = new FileStream(m_strPlistPath, FileMode.Open))
			{
				byte[] str = new byte[(int)file.Length];
				file.Read(str, 0, str.Length);
				strXmlContent = str.UTF8();
				file.Close();
				// file.Dispose();
			}

			//去掉<!DOCTYPE>,不然异常
			int delStart = strXmlContent.IndexOf("<!DOCTYPE");
			int delEnd = strXmlContent.IndexOf("\n", delStart);
			strXmlContent = strXmlContent.Remove(delStart, delEnd - delStart);
			return LoadFromXmlString(strXmlContent);
		}
        catch (Exception e) {
            Debug.LogError(e.ToString());
            return false;
        }
    }

	public bool ChangeToMultiple() 
    {
		if (!m_bHadLoad)
		{
			if (!LoadFromFile())
			{
				return false;
			}
		}

		string strTexPath = GetTexPath();
		if (strTexPath == null)
		{
			Debug.LogError("strTexPath = null");
			return false;
		}

		return NBsn.NEditor.C_EditorEx.Texture2DToMultiple(strTexPath, m_sheetMetas);
    }

	public string GetTexPath()
	{
		if (!m_bHadLoad)
		{
			Debug.LogError("not load");
			return null;
		}

		string strTexPath = Path.GetDirectoryName(m_strPlistPath) + "/" + m_strRealTextureFileName;
		Debug.Log("strTexPath:" + strTexPath);
		return strTexPath;
	}

	public static Rect StrToRect(string str)
    {
        str = str.Replace("{", "");
        str = str.Replace("}", "");
        string[] vs = str.Split(',');

        Rect v = new Rect(float.Parse(vs[0]), float.Parse(vs[1]), float.Parse(vs[2]), float.Parse(vs[3]));
        return v;
    }

    public static Vector2 StrToVec2(string str)
    {

        str = str.Replace("{","");
        str = str.Replace("}", "");
        string[] vs = str.Split(',');

        Vector2 v = new Vector2();
        v.x = float.Parse(vs[0]);
        v.y = float.Parse(vs[1]);
        return v;
    }

    private void LoadXDoc(XDocument xDoc)
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
		m_strRealTextureFileName = elements.ElementAt(3).Value; 
		Debug.LogFormat("m_strRealTextureFileName={0}", m_strRealTextureFileName);

		var strSize = elements.ElementAt(5).Value; 
		Debug.LogFormat("strSize={0}", strSize);
		m_size = StrToVec2(strSize);
    }

    private void ParseFrames(IEnumerable<XElement> elements)
    {
		var uFrameCount = elements.Count() / 2;
		m_sheetMetas = new SpriteMetaData[uFrameCount];
        for (int i = 0; i < uFrameCount; i++)
        {
            var strFrameName = elements.ElementAt(2*i).Value;
            var eDict = elements.ElementAt(2*i + 1);

			var eDictElements = eDict.Elements();
            var ePos = eDictElements.ElementAt(1);
            var eRotated = eDictElements.ElementAt(5);
            // Debug.LogFormat("frame={0} {1} eRotated.Name={2}", strFrameName, ePos.Value, eRotated.Name);
			bool bRatated = eRotated.Name == "true";

			m_sheetMetas[i].name = strFrameName;
			var rect = StrToRect(ePos.Value);
			if (bRatated) 
			{
				var tmp = rect.width;
				rect.width = rect.height;
				rect.height = tmp;
			}
			rect.y = m_size.y - (rect.y + rect.height);
			m_sheetMetas[i].rect = rect;
			m_sheetMetas[i].alignment = 0;
			m_sheetMetas[i].border = new Vector4(0, 0, 0, 0);
			m_sheetMetas[i].pivot = new Vector2(0.5f, 0.5f);
        }
    }

	public string m_strPlistPath = null;
	public bool m_bHadLoad = false;
	public string m_strRealTextureFileName = null;
	public SpriteMetaData[] m_sheetMetas = null;
	public Vector2 m_size = Vector2.zero;
}

}