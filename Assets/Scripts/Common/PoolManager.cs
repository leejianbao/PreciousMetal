using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池
/// </summary>
public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<string, List<GameObject>> poolDic = new Dictionary<string, List<GameObject>>();          //对象池字典

    /// <summary>
    /// 从对象池中获取对象
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="pos"></param>
    /// <param name="quaternion"></param>
    /// <returns></returns>
    public GameObject GetObject(ResourceType type, string name,Vector3 pos,Quaternion quaternion)
    {
        GameObject go ;
        if(poolDic.ContainsKey(name) && poolDic[name].Count>0)
        {
            go = poolDic[name][0];
            go.transform.position = pos;
            go.transform.rotation = quaternion;
            go.SetActive(true);
            poolDic[name].Remove(go);

        }
        else
        {
            go = ResourcesManager.Instance.Load(type, name);
            go.transform.position = pos;
            go.transform.rotation = quaternion;          
        }

        return go;
    }

    /// <summary>
    /// 回收对象
    /// </summary>
    /// <param name="name"></param>
    /// <param name="go"></param>
    public void RecycleObject(string name,GameObject go)
    {
        if(!poolDic.ContainsKey(name))
        {
            poolDic.Add(name, new List<GameObject>());
        }
        go.SetActive(false);
        poolDic[name].Add(go);
      
    }

    /// <summary>
    /// 清空特定对象
    /// </summary>
    /// <param name="name"></param>
    public void Clear(string name)
    {
        if(poolDic.ContainsKey(name))
        {
            for (int i = 0; i < poolDic[name].Count; i++)
            {
                Object.Destroy(poolDic[name][i]);
            }
        }
        poolDic.Remove(name);
    }

    /// <summary>
    /// 清除所有对象
    /// </summary>
    public void ClearAll()
    {
        foreach (string name in poolDic.Keys)
        {
            if(poolDic[name].Count>0)
            {
                for (int i = 0; i < poolDic[name].Count; i++)
                {
                    Object.Destroy(poolDic[name][i]);
                }
            }
            poolDic.Remove(name);
        }
    }


}
