using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

/// <summary>
/// 资源管理器
/// </summary>
public class ResourcesManager : Singleton<ResourcesManager>
{
    //预设的列表
    private Hashtable prefabTable;
    //AssetBundle取到的预设体列表
    private Hashtable assetBundlePrefabTable;

    public ResourcesManager()
    {
        prefabTable = new Hashtable();
        assetBundlePrefabTable = new Hashtable();
    }

    #region Load 加载资源
    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="name">名称</param>
    /// <param name="cache">是否加入缓存</param>
    /// <returns>预设的克隆体</returns>
    public  GameObject Load(ResourceType type, string name,bool cache=false)
    {
        GameObject go = null;

        if(prefabTable.ContainsKey(name))
        {
            go = prefabTable[name] as GameObject;
        }
        else
        {
            StringBuilder strPath = new StringBuilder();
            switch(type)
            {
                case ResourceType.WindowUI:
                    strPath.Append("WindowUI/");
                    break;
                case ResourceType.SceneUI:
                    strPath.Append("SceneUI/");
                    break;
                case ResourceType.Prefab:
                    strPath.Append("Prefab/");
                    break;
                case ResourceType.Effect:
                    strPath.Append("Effect/");
                    break;
                case ResourceType.Other:
                    strPath.Append("Other/");
                    break;
            }
            strPath.Append(name);

            go = Resources.Load<GameObject>(strPath.ToString());
            
            if(cache)
            {
                prefabTable.Add(name, go);
            }
        }

        return GameObject.Instantiate(go);
    }
    #endregion

    public GameObject LoadAssetBundle(string localFileName,string prefabName,bool cache=false)
    {
        Debug.Log(Path.Combine(LocalFileMgr.Instance.LocalABPath, localFileName));
        GameObject go = null;

        if (assetBundlePrefabTable.ContainsKey(prefabName))
        {
           
            go = assetBundlePrefabTable[prefabName] as GameObject;
        }
        else
        {
            var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(LocalFileMgr.Instance.LocalABPath, localFileName));
            if (myLoadedAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                return null;
            }

            var prefab = myLoadedAssetBundle.LoadAsset<GameObject>(prefabName);
            go = GameObject.Instantiate(prefab);
            myLoadedAssetBundle.Unload(false);
            if (cache)
            {
                assetBundlePrefabTable.Add(prefabName, go);
            }

        }

        return go;
    }


    public override void Dispose()
    {
        base.Dispose();
        prefabTable.Clear();
        assetBundlePrefabTable.Clear();

        //把未使用的资源进行释放
        Resources.UnloadUnusedAssets();

        AssetBundle.UnloadAllAssetBundles(true);
    }
}
