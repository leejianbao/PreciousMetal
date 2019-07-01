//===================
//描述：初始场景控制器
//作者：袁磊
//创建时间：2019-06-06 11:37:33
//版本：V1.0
//==================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSceneCtrl : SceneCtrlBase
{

    protected override void OnAwake()
    {
        base.OnAwake();
        UIManager.Instance.sceneUIParent = sceneUIParent;
        UIManager.Instance.windowUIparent = windowUIParent;

        UIManager.Instance.ShowSceneUI(SceneUIType.Init);
    }

    protected override void OnUpdate()
    {
        //做测试加载场景用，后面要去掉
        base.OnUpdate();
        if(Input.GetKeyDown(KeyCode.A))
        {
            SceneMgr.Instance.LoadScene(SceneType.ViewPMScene);
        }
    }
}
