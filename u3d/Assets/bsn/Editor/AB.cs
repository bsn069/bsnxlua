
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
        List<string> assetsPath = NBsn.Utils.FullPath2AssetsPath(fullPath);
        foreach (var filePath in assetsPath) {
            var importer = AssetImporter.GetAtPath(filePath);
            if (importer == null) {
                Debug.LogErrorFormat("basePath={0} importer == null", filePath);
                continue;
            }
            importer.SetAssetBundleNameAndVariant(null, null);
        }
    }

    [MenuItem("Bsn/AB/Set AB Name in dir")]
    [MenuItem("Assets/Bsn/AB/Set AB Name in dir")]
    // 将目录中可以设置ab名字的资源(路径为Assets/* )的ab名称设置为其相对路径(*.ab)
	private static void SetDirABName()
	{
        string clearPath = NBsn.NEditor.C_FolderDialog.Open("select set dir", "SetDirABName");
        List<string> fullPath = NBsn.NEditor.C_Path.GetCanSetABNameRes(clearPath);
        List<string> assetsPath = NBsn.Utils.FullPath2AssetsPath(fullPath);
        foreach (var filePath in assetsPath) {
            var importer = AssetImporter.GetAtPath(filePath);
            if (importer == null) {
                Debug.LogErrorFormat("basePath={0} importer == null", filePath);
                continue;
            }
            var strAssetsLatePath = NBsn.Utils.AssetsPath2AssetsLatePath(filePath) + ".ab";
            importer.SetAssetBundleNameAndVariant(strAssetsLatePath, null);
        }
    }

    [MenuItem("Bsn/AB/Set AB Name with dir name")]
    [MenuItem("Assets/Bsn/AB/Set AB Name with dir name")]
    // 将目录中可以设置ab名字的资源(路径为Assets/*1/*2 )的ab名称设置为其相对目录(*1.ab)
	private static void SetDirABNameToAssetsRelativeDirName()
	{
        string path = NBsn.NEditor.C_FolderDialog.Open("select set dir", "SetDirABNameToAssetsRelativeDirName");
        List<string> fullPath = NBsn.NEditor.C_Path.GetCanSetABNameRes(path);
        List<string> assetsPath = NBsn.Utils.FullPath2AssetsPath(fullPath);
        foreach (var filePath in assetsPath) {
            var importer = AssetImporter.GetAtPath(filePath);
            if (importer == null) {
                Debug.LogErrorFormat("basePath={0} importer == null", filePath);
                continue;
            }
            var strAssetsLatePathNoFile = NBsn.Utils.AssetsPath2AssetsLatePathNoFile(filePath) + ".ab";
            importer.SetAssetBundleNameAndVariant(strAssetsLatePathNoFile, null);
        }
    }

    [MenuItem("Bsn/AB/Make AB Win64")]
    [MenuItem("Assets/Bsn/AB/Make AB Win64")]
	private static void MakeABWin() 
    {
        string strFullPath = NBsn.NEditor.C_FolderDialog.Save("select out dir", "MakeABWin");
        MakeAB(BuildTarget.StandaloneWindows64, strFullPath);
    }

    public static void MakeAB(BuildTarget buildTarget, string strOutFullPath) {
        Debug.LogFormat("buildTarget={0} strOutFullPath={1}", buildTarget, strOutFullPath); 

        Directory.CreateDirectory(strOutFullPath);

        //AssetDatabase.Refresh();
        BuildPipeline.BuildAssetBundles(strOutFullPath, BuildAssetBundleOptions.UncompressedAssetBundle, buildTarget);
        //AssetDatabase.Refresh();
    } 
}

}
