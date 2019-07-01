//脚本创建时添加备注信息，方便溯源
//================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

public class ScriptTemplate : UnityEditor.AssetModificationProcessor
{
    private static string str =
        "//===================\r\n"
        + "//描述：\r\n"
        + "//作者：#AuthorName#\r\n"
        + "//创建时间：#CreateTime#\r\n"
        + "//版本：V1.0\r\n"
        + "//==================\r\n";

    private static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (path.EndsWith(".cs"))
        {
            string strContent = str;
            strContent += File.ReadAllText(path);
            strContent = strContent.Replace("#CreateTime#", DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"));
            File.WriteAllText(path, strContent);
            AssetDatabase.Refresh();
        }
    }

}
