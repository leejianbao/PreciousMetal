using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景管理器
/// </summary>
public class SceneMgr : Singleton<SceneMgr>
{
    /// <summary>
    /// 当前场景类型
    /// </summary>
    public SceneType CurrentSceneType
    {
        get;
        private set;
    }

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="type"></param>
    public void LoadScene(SceneType type)
    {
        CurrentSceneType = type;
        SceneManager.LoadScene(type.ToString());
    }
}
