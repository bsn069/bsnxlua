
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

public static class C_Asset
{
    [MenuItem("Assets/Bsn/Asset/Find who direct dependencies me")]
	private static void FindWhoDirectDependenciesMe()
	{
        string assetPath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
        //Debug.LogFormat("assetPath={0}", assetPath);
        string findPath = NBsn.NEditor.C_FolderDialog.Open("select find dir", "FindWhoDirectDependenciesMe");
        //Debug.LogFormat("findPath={0}", findPath);
        List<string> fullPath = GetDependenciesRes(findPath);
        List<string> assetsPath = FullPath2AssetsPath(fullPath);
        foreach (var filePath in assetsPath) {
            string[] deps = AssetDatabase.GetDependencies(filePath, false);
            foreach (var file in deps) {
                if (file.CompareTo(assetPath) == 0) { // dependencies me
                    Debug.LogFormat(filePath);
                    break;
                }
            }
        }
    }

    [MenuItem("Assets/Bsn/Asset/Find direct dependencies")]
	private static void FindDirectDependencies()
	{
		string path = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
        Debug.LogFormat("path={0}", path);
        string[] deps = AssetDatabase.GetDependencies(path, false);
        foreach (var item in deps) {
            Debug.LogFormat(item);
        }
    }

    [MenuItem("Assets/Bsn/Asset/Show select ojbect")]
	private static void ShowSelectObject()
	{
        Debug.LogFormat("Selection.activeGameObject={0}", Selection.activeGameObject);
        Debug.LogFormat("Selection.activeInstanceID={0}", Selection.activeInstanceID);
        Debug.LogFormat("Selection.activeObject={0}", Selection.activeObject);
        Debug.LogFormat("Selection.assetGUIDs={0}", Selection.assetGUIDs);
        Debug.LogFormat("Selection.gameObjects={0}", Selection.gameObjects);
        Debug.LogFormat("Selection.instanceIDs={0}", Selection.instanceIDs);
        Debug.LogFormat("Selection.transforms={0}", Selection.transforms);
        Debug.LogFormat("Selection.activeTransform={0}", Selection.activeTransform);
    }

    // 获取path里会依赖其它资源的资源
    // ["H:/dev/swordm3d-code/trunk/client/swordm3d/Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab", ...]
    public static List<string> GetDependenciesRes(string path)
	{
        //Debug.LogFormat("GetDependenciesRes({0})", path);
        List<string> allFiles = new List<string>();
        string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
        if (files != null) {
            for (int i = 0; i < files.Length; ++i) {
                string file = files[i];
                if (file.EndsWith(".cs")
                    || file.EndsWith(".meta")
                    || file.EndsWith(".png")
                ) {
                    continue;
                }
                file = file.Replace('\\', '/');
                allFiles.Add(file);
            }
        }
        return allFiles;
    }

    // 返回全路径的Assets路径
    // path ["H:/dev/swordm3d-code/trunk/client/swordm3d/Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab", ...]
    // ret ["Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab", ...]
    public static List<string> FullPath2AssetsPath(List<string> path) 
    {
        var subStart = Application.dataPath.Length - "Assets".Length;
        List<string> ret = new List<string>();
        foreach (var item in path) {
            ret.Add(item.Substring(subStart));
        }
        return ret;
    }
}

}
