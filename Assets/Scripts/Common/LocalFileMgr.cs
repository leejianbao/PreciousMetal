using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// 本地文件管理
/// </summary>
public class LocalFileMgr : Singleton<LocalFileMgr>
{
#if UNITY_EDITOR
    public readonly string LocalFilePath = Application.dataPath + "/../AssetBundles/";
    public readonly string LocalModelPath = Application.dataPath + "/../AssetBundles/models/";
#else
    public readonly string LocalFilePath = Application.persistentDataPath + "/";
    public readonly string LocalModelPath = Application.persistentDataPath + "/models/";
#endif

    public readonly string LocalABPath = Application.streamingAssetsPath;


    /// <summary>
    /// 读取本地文件到数组
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public byte[] GetBuffer(string path)
    {
        byte[] buffer = null;
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
        }
        return buffer;
    }

    
}
