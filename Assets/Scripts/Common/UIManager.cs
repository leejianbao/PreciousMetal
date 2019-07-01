using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI管理器
/// </summary>
public class UIManager : SingletonMono<UIManager>
{
    private Dictionary<SceneUIType, GameObject> sceneUIDic = new Dictionary<SceneUIType, GameObject>();           //场景UI字典
    private Dictionary<WindowUIType, GameObject> windowUIDic = new Dictionary<WindowUIType, GameObject>();        //窗口UI字典

    public Transform sceneUIParent;                 //场景UI的父节点(需要在场景的初始化中赋值下，下同)
    public Transform windowUIparent;                //窗口UI的父节点
    public Transform currentUIWindow;               //当前UI界面
    /// <summary>
    /// 显示场景UI
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject ShowSceneUI(SceneUIType type)
    {
        GameObject go = null;

        if(!sceneUIDic.ContainsKey(type))
        {
            //枚举名称要与预设名称对应
            go = ResourcesManager.Instance.Load(ResourceType.SceneUI,type.ToString());
            if (go == null)
                return null;

            sceneUIDic.Add(type, go);

            go.transform.parent = sceneUIParent;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.transform.eulerAngles = Vector3.zero;

        }
        else
        {
            go = sceneUIDic[type];
        }

        go.SetActive(true);
        return go;
    }

    /// <summary>
    /// 关闭场景UI
    /// </summary>
    /// <param name="type"></param>
    public void CloseSceneUI(SceneUIType type)
    {
        if(sceneUIDic.ContainsKey(type))
        {
            sceneUIDic[type].SetActive(false);
        }
    }

    /// <summary>
    /// 显示窗口UI
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject ShowWindowUI(WindowUIType type)
    {
        GameObject go = null;

        if (!windowUIDic.ContainsKey(type))
        {
            //枚举名称要与预设名称对应
            go = ResourcesManager.Instance.Load(ResourceType.WindowUI, type.ToString());
            if (go == null)
                return null;

            windowUIDic.Add(type, go);

            go.transform.parent = windowUIparent;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.transform.eulerAngles = Vector3.zero;

        }
        else
        {
            go = windowUIDic[type];
        }

        go.SetActive(true);
        return go;
    }

    /// <summary>
    /// 关闭UI窗口
    /// </summary>
    /// <param name="type"></param>
    public void CloseWindowUI(WindowUIType type)
    {
        if(windowUIDic.ContainsKey(type))
        {
            windowUIDic[type].SetActive(false);
        }
    }

    /// <summary>
    /// 关闭所有UI窗口
    /// </summary>
    public void CloseAllWindowUI()
    {
        foreach (GameObject go in windowUIDic.Values)
        {
            if (go != null)
            {
                go.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 销毁并清空所有UI面板
    /// </summary>
    public void ClearAll()
    {
        foreach (GameObject go in sceneUIDic.Values)
        {
            if(go!=null)
            {
                Object.Destroy(go);
            }
        }
        sceneUIDic.Clear();

        foreach (GameObject go in windowUIDic.Values)
        {
            if(go!=null)
            {
                Object.Destroy(go);
            }
        }
        windowUIDic.Clear();
    }
	
}
