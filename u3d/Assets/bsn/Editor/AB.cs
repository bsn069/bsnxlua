
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

public static class C_AB
{
    [MenuItem("Bsn/AB/Clean AB Name in dir")]
    [MenuItem("Assets/Bsn/AB/Clean AB Name in dir")]
	private static void CleanDirABName()
	{
        string clearPath = NBsn.NEditor.C_FolderDialog.Open("select clean dir", "CleanDirABName");
        List<string> fullPath = NBsn.NEditor.C_Path.GetCanSetABNameRes(clearPath);
        List<string> assetsPath = fullPath.FullPaths2AssetsPaths();
        foreach (var filePath in assetsPath) {
            var importer = AssetImporter.GetAtPath(filePath);
            if (importer == null) {
                Debug.LogErrorFormat("basePath={0} importer == null", filePath);
                continue;
            }
            importer.SetAssetBundleNameAndVariant(null, null);
        }
    }
}

}
