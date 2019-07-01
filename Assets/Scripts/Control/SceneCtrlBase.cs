//===================
//描述：场景控制基类
//作者：袁磊
//创建时间：2019-06-06 13:32:28
//版本：V1.0
//==================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCtrlBase : MonoBehaviour {

    [SerializeField]
    protected Transform sceneUIParent;                  //场景UI挂载点的引用
    [SerializeField]
    protected Transform windowUIParent;                 //窗口UI挂载点的引用

    void Awake()
    {
        OnAwake();
    }

    void Start()
    {
        OnStart();
    }


    void Update()
    {
        OnUpdate();
    }

    void OnDestroy()
    {
        BeforeOnDestroy();
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
    protected virtual void BeforeOnDestroy() { }
}
