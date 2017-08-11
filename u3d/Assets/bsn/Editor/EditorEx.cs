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

public static class C_EditorEx
{
	[MenuItem("GameObject/Bsn/Texture/拆解", false, 1)]
    [MenuItem("Assets/Bsn/Texture/拆解")]
    [MenuItem("Bsn/Texture/拆解")]
    static void SplitMultiple() 
    {
		string strAssetPath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);

        const string c_strPathKey = "NBsn.NEditor.C_EditorEx.SplitMultiple";
        string path = NBsn.NEditor.C_FolderDialog.Save("select atlas output dir", c_strPathKey);
        Debug.LogFormat("save dir={0}", path);
		MultipleTexture2DSplit(strAssetPath, path);
    }

	public static bool Texture2DToMultiple(string strTexPath, SpriteMetaData[] sheetMetas) 
    {
		if (strTexPath == null)
		{
			Debug.LogError("strTexPath = null");
			return false;
		}

		if (sheetMetas == null)
		{
			Debug.LogError("sheetMetas = null");
			return false;
		}

        Texture2D tex2D = AssetDatabase.LoadAssetAtPath(strTexPath, typeof(Texture2D)) as Texture2D;
		if (tex2D == null)
		{
			Debug.LogErrorFormat("strTexPath={0} not found", strTexPath);
			return false;
		}

		TextureImporter textureImporter = AssetImporter.GetAtPath(strTexPath) as TextureImporter;
		textureImporter.textureType = TextureImporterType.Sprite;
		textureImporter.spriteImportMode = SpriteImportMode.Multiple;
		textureImporter.spritesheet = sheetMetas;

		AssetDatabase.ImportAsset(strTexPath, ImportAssetOptions.ForceUpdate);
		return true;
    }

	public static bool MultipleTexture2DSplit(string strTexPath, string strOutDir) 
    {
		if (strTexPath == null)
		{
			Debug.LogError("strTexPath = null");
			return false;
		}

		if (strOutDir == null)
		{
			Debug.LogError("strOutDir = null");
			return false;
		}

        Texture2D tex2D = AssetDatabase.LoadAssetAtPath(strTexPath, typeof(Texture2D)) as Texture2D;
		if (tex2D == null)
		{
			Debug.LogErrorFormat("strTexPath={0} not found", strTexPath);
			return false;
		}

		TextureImporter textureImporter = AssetImporter.GetAtPath(strTexPath) as TextureImporter;

		if (textureImporter.spriteImportMode != SpriteImportMode.Multiple)
		{
			Debug.LogError("spriteImportMode not Multiple");
			return false;
		}

		if (textureImporter.textureType != TextureImporterType.Sprite)
		{
			Debug.LogError("textureType not Sprite");
			return false;
		}

		bool bSetReadWriteAttr = false;
		if (!textureImporter.isReadable)
		{
			bSetReadWriteAttr = true;
			textureImporter.isReadable = true;
			AssetDatabase.ImportAsset(strTexPath, ImportAssetOptions.ForceUpdate);		
		}

		Directory.CreateDirectory(strOutDir);
		var color32 = tex2D.GetPixels32();
		var spritesheet = textureImporter.spritesheet;
		foreach (var item in spritesheet)
		{
			SavePNG(color32, tex2D.width, tex2D.height, item, strOutDir);
		}

		if (bSetReadWriteAttr)
		{
			textureImporter.isReadable = false;
			AssetDatabase.ImportAsset(strTexPath, ImportAssetOptions.ForceUpdate);		
		}

		return true;
    }

	public static bool SavePNG(
		Color32[] pixels
		, int width
		, int height
		, SpriteMetaData metaData
		, string strOutDir
	) 
	{
		Color32[] newPixels = new Color32[(int)metaData.rect.width * (int)metaData.rect.height];
        var tex = new Texture2D((int)metaData.rect.width, (int)metaData.rect.height);

        int ymin = (int)metaData.rect.y;
        int ymax = ymin + (int)metaData.rect.height;
        int index = 0;
        for (int y = ymin; y < ymax; y++) {
            for (int x = (int)metaData.rect.x; x < (int)metaData.rect.x + (int)metaData.rect.width ; x++) {
                newPixels[index++] = pixels[y*width + x];
            }
        }

        tex.SetPixels32(newPixels);
		tex.Apply();
		var strFileName = metaData.name.Replace('\\', '_').Replace('/', '_');
		var strFilePath = strOutDir.PathCombine(strFileName + ".png");
        File.WriteAllBytes(strFilePath, tex.EncodeToPNG());
		return true;
	}	

	// strAssetTexPath Assets/*/?.png
	// strSpritePackingTag
	public static bool SetSpritePackingTag(string strAssetTexPath, string strSpritePackingTag) 
    {
		if (strAssetTexPath == null)
		{
			Debug.LogError("strAssetTexPath = null");
			return false;
		}

		if (strSpritePackingTag == null)
		{
			Debug.LogError("strSpritePackingTag = null");
			return false;
		}

        Texture2D tex2D = AssetDatabase.LoadAssetAtPath(strAssetTexPath, typeof(Texture2D)) as Texture2D;
		if (tex2D == null)
		{
			Debug.LogErrorFormat("strAssetTexPath={0} not found", strAssetTexPath);
			return false;
		}

		TextureImporter textureImporter = AssetImporter.GetAtPath(strAssetTexPath) as TextureImporter;
		textureImporter.spritePackingTag = strSpritePackingTag;

		AssetDatabase.ImportAsset(strAssetTexPath, ImportAssetOptions.ForceUpdate);
		return true;
    }

}

}