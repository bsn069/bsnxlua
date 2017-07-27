
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace NBsn.NEditor 
{

public class C_NGUIAtlas 
{
	[MenuItem("GameObject/Bsn/NGUIAtlas/修改为Multiple", false, 1)]
	[MenuItem("Assets/Bsn/NGUIAtlas/修改为Multiple")]
    [MenuItem("Bsn/NGUIAtlas/修改为Multiple")]
	public static void ChangeToMultiple_s() 
    {
        string strAssetPath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
		ms_cNGUIAtlas.SetAssetPath(strAssetPath);
		ms_cNGUIAtlas.LoadUIAtlas();
		ms_cNGUIAtlas.LoadTexData();
		ms_cNGUIAtlas.ParseSprites();
		ms_cNGUIAtlas.ChangeToMultiple();
    }

	[MenuItem("GameObject/Bsn/NGUIAtlas/修改为Multiple", true)]
	[MenuItem("Assets/Bsn/NGUIAtlas/修改为Multiple", true)]
    [MenuItem("Bsn/NGUIAtlas/修改为Multiple", true)]
	public static bool ChangeToMultiple_c() 
    {
        string strAssetPath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
		ms_cNGUIAtlas.SetAssetPath(strAssetPath);
		return ms_cNGUIAtlas.LoadUIAtlas();
    }
	public static C_NGUIAtlas ms_cNGUIAtlas = new C_NGUIAtlas();

    public bool SetAssetPath(string strAssetPath)
	{
		m_strAssetPath = strAssetPath;
		return true;
	}	

	public bool ChangeToMultiple() 
    {
		if (m_sheetMetas == null)
		{
            Debug.LogError("m_sheetMetas == null");
			return false;
		}

		if (m_strTexPath == null)
		{
            Debug.LogError("m_strTexPath == null");
			return false;
		}

		return NBsn.NEditor.C_EditorEx.Texture2DToMultiple(m_strTexPath, m_sheetMetas);
    }

	public bool LoadUIAtlas()
    {
		if (m_strAssetPath == null)
		{
            Debug.LogError("m_strAssetPath == null");
			return false;
		}

		try 
		{
			GameObject go = AssetDatabase.LoadAssetAtPath(m_strAssetPath, typeof(GameObject)) as GameObject;
			if (go == null)
			{
				return false;
			}

			m_uiAtlas = go.transform.GetComponent<UIAtlas>();
			if (m_uiAtlas == null) {
				return false;
			}

			return true;
		}
        catch (Exception e) {
            Debug.LogError(e.ToString());
            return false;
        }
    }

	public bool LoadTexData()
	{
		if (m_uiAtlas == null)
		{
            Debug.LogError("m_uiAtlas == null");
			return false;
		}

		try 
		{
			Material spriteMaterial = m_uiAtlas.spriteMaterial;
			if (spriteMaterial == null) {
				Debug.LogErrorFormat("spriteMaterial == null");
				return false;
			}

			Texture mainTexture = spriteMaterial.mainTexture;
			if (mainTexture == null) {
				Debug.LogErrorFormat("mainTexture == null");
				return false;
			}

			m_size = new Vector2(mainTexture.width, mainTexture.height);
			Debug.LogFormat("m_size:{0}", m_size);

			m_strTexPath = AssetDatabase.GetAssetPath(mainTexture.GetInstanceID());
			Debug.Log("m_strTexPath:" + m_strTexPath);
			return true;
		}
        catch (Exception e) {
            Debug.LogError(e.ToString());
            return false;
        }
	}

	public bool ParseSprites()
	{
		if (m_uiAtlas == null)
		{
            Debug.LogError("m_uiAtlas == null");
			return false;
		}

		if (m_size == Vector2.zero)
		{
            Debug.LogError("m_size == Vector2.zero");
			return false;
		}

		try 
		{
			m_sheetMetas = new SpriteMetaData[m_uiAtlas.spriteList.Count];
			for (int i = 0; i < m_uiAtlas.spriteList.Count; i++) 
			{
				UISpriteData spriteData = m_uiAtlas.spriteList[i];
				m_sheetMetas[i].name = spriteData.name;
				m_sheetMetas[i].rect = new Rect(
					spriteData.x
					, m_size.y - (spriteData.y + spriteData.height)
					, spriteData.width
					, spriteData.height
				);
				m_sheetMetas[i].alignment = 0;
				m_sheetMetas[i].border = new Vector4(
					spriteData.borderLeft
					, spriteData.borderTop
					, spriteData.borderRight
					, spriteData.borderBottom
				);
				m_sheetMetas[i].pivot = new Vector2(0.5f, 0.5f);
			}

			return true;
		}
        catch (Exception e) {
            Debug.LogError(e.ToString());
            return false;
        }
	}

	public string 	m_strAssetPath 	= null;
	public UIAtlas 	m_uiAtlas 		= null;
	public Vector2 	m_size 			= Vector2.zero;
	public string 	m_strTexPath 	= null;
	public SpriteMetaData[] m_sheetMetas = null;



    // [MenuItem("GameObject/Bsn/NGUIAtlas/拆解", false, 49)]
    // [MenuItem("Assets/Bsn/NGUIAtlas/拆解")]
    // [MenuItem("Bsn/NGUIAtlas/拆解")]
    // static void SelectObj() 
    // {
    //     GameObject go = Selection.activeObject as GameObject;
    //     if (go == null) {
    //         Debug.LogErrorFormat("not select gameobject");
    //         return;
    //     }

    //     UIAtlas uiAtlas = go.transform.GetComponent<UIAtlas>();
    //     if (uiAtlas == null) {
    //         Debug.LogErrorFormat("not a UIAtlas");
    //         return;
    //     }

    //     const string c_strPathKey = "NBsnEditor.C_NGUIAtlas.Test.SavePath";
    //     string path = NBsn.NEditor.C_FolderDialog.Save("select atlas output dir", c_strPathKey);
    //     Debug.LogFormat("save dir={0}", path);
    //     var outDir = SaveAtlas(uiAtlas, path);
    //     if (outDir == null) {
    //         Debug.LogErrorFormat("outDir == null");
    //         return;
    //     }

    //     // open output dir
    //     EditorUtility.OpenWithDefaultApp(outDir);
    // }

    // [MenuItem("GameObject/Bsn/NGUIAtlas/拆解", true)]
    // [MenuItem("Assets/Bsn/NGUIAtlas/拆解", true)]
    // [MenuItem("Bsn/NGUIAtlas/拆解", true)]
    // static bool SelectObjValidate() {
    //     GameObject go = Selection.activeObject as GameObject;
    //     if (go == null) {
    //         return false;
    //     }

    //     UIAtlas uiAtlas = go.transform.GetComponent<UIAtlas>();
    //     if (uiAtlas == null) {
    //         return false;
    //     }

    //     return true;
    // }


    // // return outDir
    // static string SaveAtlas(UIAtlas uiAtlas, string strOutRootDir) {
    //     if (string.IsNullOrEmpty(strOutRootDir)) {
    //         Debug.LogErrorFormat("not set Out Dir");
    //         return null;
    //     }

    //     Material spriteMaterial = uiAtlas.spriteMaterial;
    //     if (spriteMaterial == null) {
    //         Debug.LogErrorFormat("spriteMaterial == null");
    //         return null;
    //     }

    //     Texture mainTexture = spriteMaterial.mainTexture;
    //     if (mainTexture == null) {
    //         Debug.LogErrorFormat("mainTexture == null");
    //         return null;
    //     }

    //     Texture2D t2d = NGUIEditorTools.ImportTexture(mainTexture, true, false, false);
    //     string path = AssetDatabase.GetAssetPath(mainTexture.GetInstanceID());

    //     string outDir = string.Format("{0}/{1}", strOutRootDir, Path.GetFileNameWithoutExtension(path));
    //     string dirAtlas = outDir + "/atlas";
    //     Directory.CreateDirectory(dirAtlas);
    //     File.WriteAllBytes(dirAtlas + "/" + Path.GetFileName(path), t2d.EncodeToPNG());

    //     string dirSprite = outDir + "/sprite";
    //     Directory.CreateDirectory(dirSprite);
    //     var color32 = t2d.GetPixels32();
    //     for (int i = 0; i < uiAtlas.spriteList.Count; i++) {
    //         UISpriteData spriteData = uiAtlas.spriteList[i];
    //         SaveSprite(uiAtlas.spriteList[i], color32, t2d.width, t2d.height, dirSprite + "/" + spriteData.name + ".png");
    //     }
    //     return outDir;
    // }

    // static void SaveSprite(UISpriteData sd, Color32[] oldPixels, int oldWidth, int oldHeight, string path) {
    //     Color32[] newPixels = new Color32[sd.width * sd.height];
    //     var tex = new Texture2D(sd.width, sd.height);

    //     int ymax = oldHeight - sd.y;
    //     int ymin = ymax - sd.height;
    //     int index = 0;
    //     for (int y = ymin; y < ymax; y++) {
    //         for (int x = sd.x; x < sd.x + sd.width ; x++) {
    //             newPixels[index++] = oldPixels[y*oldWidth + x];
    //         }
    //     }

    //     tex.SetPixels32(newPixels);
	// 	tex.Apply();
    //     File.WriteAllBytes(path, tex.EncodeToPNG());
    // }
}

}
