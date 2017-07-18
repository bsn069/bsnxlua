using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace NBsn.NEditor 
{

public static class C_FolderDialog 
{
    // show open folder dialog
    // strPathSaveKey for record last path, default open strPathSaveKey key record path 
    public static string Open(string strTitle = "", string strPathSaveKey = null) {
        var strPath = "";
        if (strPathSaveKey != null) 
        {
            strPath = PlayerPrefs.GetString(strPathSaveKey);
            if (string.IsNullOrEmpty(strPath)) {
                strPath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);;
                if (string.IsNullOrEmpty(strPath)) {
                    strPath = Application.dataPath;
                }
            }
        }

        strPath = EditorUtility.OpenFolderPanel(strTitle, strPath, "");

        if (strPathSaveKey != null) 
        {
            PlayerPrefs.SetString(strPathSaveKey, strPath);
        }

        Debug.LogFormat(strPath);
        return strPath;
    }

    // show save folder dialog
    // strPathSaveKey for record last path, default save strPathSaveKey key record path 
    public static string Save(string strTitle = "", string strPathSaveKey = null) {
        var strPath = "";
        if (strPathSaveKey != null) 
        {
            strPath = PlayerPrefs.GetString(strPathSaveKey);
            if (string.IsNullOrEmpty(strPath)) {
                strPath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);;
                if (string.IsNullOrEmpty(strPath)) {
                    strPath = Application.dataPath;
                }
            }
        }

        strPath = EditorUtility.SaveFolderPanel(strTitle, strPath, "");

        if (strPathSaveKey != null) 
        {
            PlayerPrefs.SetString(strPathSaveKey, strPath);
        }

        Debug.LogFormat(strPath);
        return strPath;
    }
}

}
