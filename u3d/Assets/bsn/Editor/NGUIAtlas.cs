
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace NBsn.NEditor 
{

public static class C_NGUIAtlas 
{
    [MenuItem("GameObject/Bsn/NGUIAtlas/拆解", false, 49)]
    [MenuItem("Assets/Bsn/NGUIAtlas/拆解")]
    [MenuItem("Bsn/NGUIAtlas/拆解")]
    static void SelectObj() 
    {
        GameObject go = Selection.activeObject as GameObject;
        if (go == null) {
            Debug.LogErrorFormat("not select gameobject");
            return;
        }

        UIAtlas uiAtlas = go.transform.GetComponent<UIAtlas>();
        if (uiAtlas == null) {
            Debug.LogErrorFormat("not a UIAtlas");
            return;
        }

        const string c_strPathKey = "NBsnEditor.C_NGUIAtlas.Test.SavePath";
        string path = NBsn.NEditor.C_FolderDialog.Save("select atlas output dir", c_strPathKey);
        Debug.LogFormat("save dir={0}", path);
        var outDir = SaveAtlas(uiAtlas, path);
        if (outDir == null) {
            Debug.LogErrorFormat("outDir == null");
            return;
        }

        // open output dir
        EditorUtility.OpenWithDefaultApp(outDir);
    }

    [MenuItem("GameObject/Bsn/NGUIAtlas/拆解", true)]
    [MenuItem("Assets/Bsn/NGUIAtlas/拆解", true)]
    [MenuItem("Bsn/NGUIAtlas/拆解", true)]
    static bool SelectObjValidate() {
        GameObject go = Selection.activeObject as GameObject;
        if (go == null) {
            return false;
        }

        UIAtlas uiAtlas = go.transform.GetComponent<UIAtlas>();
        if (uiAtlas == null) {
            return false;
        }

        return true;
    }


    // return outDir
    static string SaveAtlas(UIAtlas uiAtlas, string strOutRootDir) {
        if (string.IsNullOrEmpty(strOutRootDir)) {
            Debug.LogErrorFormat("not set Out Dir");
            return null;
        }

        Material spriteMaterial = uiAtlas.spriteMaterial;
        if (spriteMaterial == null) {
            Debug.LogErrorFormat("spriteMaterial == null");
            return null;
        }

        Texture mainTexture = spriteMaterial.mainTexture;
        if (mainTexture == null) {
            Debug.LogErrorFormat("mainTexture == null");
            return null;
        }

        Texture2D t2d = NGUIEditorTools.ImportTexture(mainTexture, true, false, false);
        string path = AssetDatabase.GetAssetPath(mainTexture.GetInstanceID());

        string outDir = string.Format("{0}/{1}", strOutRootDir, Path.GetFileNameWithoutExtension(path));
        string dirAtlas = outDir + "/atlas";
        Directory.CreateDirectory(dirAtlas);
        File.WriteAllBytes(dirAtlas + "/" + Path.GetFileName(path), t2d.EncodeToPNG());

        string dirSprite = outDir + "/sprite";
        Directory.CreateDirectory(dirSprite);
        var color32 = t2d.GetPixels32();
        for (int i = 0; i < uiAtlas.spriteList.Count; i++) {
            UISpriteData spriteData = uiAtlas.spriteList[i];
            SaveSprite(uiAtlas.spriteList[i], color32, t2d.width, t2d.height, dirSprite + "/" + spriteData.name + ".png");
        }
        return outDir;
    }

    static void SaveSprite(UISpriteData sd, Color32[] oldPixels, int oldWidth, int oldHeight, string path) {
        Color32[] newPixels = new Color32[sd.width * sd.height];
        var tex = new Texture2D(sd.width, sd.height);

        int ymax = oldHeight - sd.y;
        int ymin = ymax - sd.height;
        int index = 0;
        for (int y = ymin; y < ymax; y++) {
            for (int x = sd.x; x < sd.x + sd.width ; x++) {
                newPixels[index++] = oldPixels[y*oldWidth + x];
            }
        }

        tex.SetPixels32(newPixels);
		tex.Apply();
        File.WriteAllBytes(path, tex.EncodeToPNG());
    }
}

}
