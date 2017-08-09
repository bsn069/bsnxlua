
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;

namespace NBsn.NEditor 
{

public static class C_BsnAtlas
{
    [MenuItem("Bsn/Bsn/Set Atlas Sprite Packing Tag")]
	private static void SetSpritePackingTag()
	{
		var listAtlasFileFullPaths = NBsn.NEditor.C_Path.GetAtlasFileFullPaths();
		List<string> listAssetsPaths = listAtlasFileFullPaths.FullPaths2AssetsPaths();
        foreach (var strAssetsPath in listAssetsPaths) {
			NBsn.NEditor.C_EditorEx.SetSpritePackingTag(strAssetsPath, strAssetsPath.PathUpDirName());
        }
    }
}

}
