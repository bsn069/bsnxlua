
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

public static class C_Path
{
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


    // 获取path里可以设置ab名称的资源
    // ["H:/dev/swordm3d-code/trunk/client/swordm3d/Assets/_Game/Resources/Packages/UI/EquipAvartarTip.prefab", ...]
    public static List<string> GetCanSetABNameRes(string path)
	{
        //Debug.LogFormat("GetDependenciesRes({0})", path);
        List<string> allFiles = new List<string>();
        string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
        if (files != null) {
            for (int i = 0; i < files.Length; ++i) {
                string file = files[i];
                if (file.EndsWith(".cs")
                    || file.EndsWith(".meta")
                    || file.EndsWith(".ab")
                ) {
                    continue;
                }
                file = file.Replace('\\', '/');
                allFiles.Add(file);
            }
        }
        return allFiles;
    }
}

}
